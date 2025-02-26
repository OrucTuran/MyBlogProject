using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.EntityLayer.Concrete;
using MyBlogNight.PresentationLayer.Areas.Author.Models;
using MyBlogNight.PresentationLayer.Models;
using Newtonsoft.Json.Linq;

namespace MyBlogNight.PresentationLayer.Areas.Author.Controllers
{
    [Area("Author")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BlogContext _context;
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        public ProfileController(UserManager<AppUser> userManager, BlogContext context, IArticleService articleService, ICategoryService categoryService)
        {
            _userManager = userManager;
            _context = context;
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var _context = new BlogContext();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.FullName = user.Name + " " + user.Surname;
            ViewBag.email = user.Email;
            ViewBag.description = user.AppUserDescription;
            ViewBag.name = user.Name;
            ViewBag.surname = user.Surname;
            ViewBag.username = user.UserName;

            // Toplam blog yazısı sayısı
            var totalArticles = await _context.Articles
                .Where(x => x.AppUserId == user.Id)
                .CountAsync();
            ViewBag.TotalArticles = totalArticles;

            // Her bir makale için toplam yorum sayısını almak
            var commentCounts = await _context.Articles
                .Where(x => x.AppUserId == user.Id)
                .Select(x => x.Comments.Count)
                .ToListAsync();
            var totalComments = commentCounts.Sum();
            ViewBag.TotalComments = totalComments;

            // Her bir makale için toplam görüntülenme sayısını almak
            var viewCounts = await _context.Articles
                .Where(x => x.AppUserId == user.Id)
                .Select(x => x.ArticleViewCount ?? 0)
                .ToListAsync();
            var totalViews = viewCounts.Sum();
            ViewBag.TotalViews = totalViews;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditMyProfile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel model = new UserEditViewModel();
            model.Name = values.Name;
            model.Surname = values.Surname;
            model.Username = values.UserName;
            model.Email = values.Email;
            model.AppUserDescription = values.AppUserDescription;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserEditViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Username;
            user.AppUserDescription = model.AppUserDescription;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> NewBlogByAuthor(NewBlogViewModel model, IFormFile CoverImage)
        {
            if (ModelState.IsValid)
            {
                var _context = new BlogContext();
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var newArticle = new Article
                {
                    Title = model.Title,
                    Detail = model.Detail,
                    CategoryId = model.CategoryId,
                    AppUserId = user.Id,
                    CreatedDate = DateTime.Now,
                    ArticleViewCount = 0,
                    MainImageUrl = "test"
                };

                if (CoverImage != null && CoverImage.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", CoverImage.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await CoverImage.CopyToAsync(stream);
                    }
                    newArticle.CoverImageUrl = "/images/" + CoverImage.FileName;
                }

                _context.Articles.Add(newArticle);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Post başarıyla eklendi!", redirectUrl = Url.Action("Index", "Profile", new { area = "Author" }) });
            }

            return Json(new { success = false, message = "Lütfen tüm alanları doldurun!" });
        }

        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return RedirectToAction("Index", "Profile", new { area = "Author" });
            }

            // Kategorileri çekiyoruz ve viewModel'e ekliyoruz
            var categories = await _context.Categories
                                           .Select(c => new SelectListItem
                                           {
                                               Value = c.CategoryId.ToString(),
                                               Text = c.CategoryName
                                           }).ToListAsync();

            var model = new EditBlogViewModel
            {
                ArticleId = article.ArticleId,
                Title = article.Title,
                Detail = article.Detail,
                CategoryId = article.CategoryId,
                CoverImageUrl = article.CoverImageUrl,
                Categories = categories // Kategoriler burada
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(EditBlogViewModel model, IFormFile CoverImage)
        {
          
                var _context = new BlogContext();
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                // Mevcut makaleyi bulalım
                var existingArticle = await _context.Articles.FindAsync(model.ArticleId);

                if (existingArticle == null)
                {
                    return Json(new { success = false, message = "Blog bulunamadı!" });
                }

                // Makale verilerini güncelle
                existingArticle.Title = model.Title;
                existingArticle.Detail = model.Detail;
                existingArticle.CategoryId = model.CategoryId;
                existingArticle.AppUserId = user.Id;
                existingArticle.CreatedDate = existingArticle.CreatedDate; // Bu alan değiştirilmesin
                existingArticle.ArticleViewCount = existingArticle.ArticleViewCount; // Bu da değişmesin

                // Resim varsa, dosya yükleme işlemi
                if (CoverImage != null && CoverImage.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", CoverImage.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await CoverImage.CopyToAsync(stream);
                    }
                    existingArticle.CoverImageUrl = "/images/" + CoverImage.FileName;
                }

                // Veritabanını güncelle
                _context.Articles.Update(existingArticle);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Post başarıyla güncellendi!", redirectUrl = Url.Action("Index", "Profile", new { area = "Author" }) });
        

            
        }




        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return Json(new { success = false, message = "Blog bulunamadı!" });
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Blog başarıyla silindi!" });
        }
    }
}
