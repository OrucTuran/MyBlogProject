using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ProfileController(UserManager<AppUser> userManager, BlogContext context)
        {
            _userManager = userManager;
            _context = context;
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

    }
}
