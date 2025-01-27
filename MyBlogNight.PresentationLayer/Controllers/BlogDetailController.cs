using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public BlogDetailController(IArticleService articleService, ICommentService commentService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _articleService = articleService;
            _commentService = commentService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> BlogDetails(int id)
        {
            var value = _articleService.TArticleListWithCategoryAndAppUserByArticleId(id);
            var user = await _userManager.GetUserAsync(User);
            ViewData["UserName"] = user != null ? user.UserName : null;
            ViewData["CurrentArticleId"] = id;  // id'yi ViewData'ya ekliyoruz
            return View(value);
        }
        public async Task<IActionResult> LogOut(int id)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(BlogDetails), new { id = id });  // Çıkış sonrası doğru blog id'sine yönlendirme
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment, int articleId)
        {
            comment.CreatedDate = DateTime.Now;
            comment.Status = true;

            comment.ArticleId = articleId;

            _commentService.TInsert(comment);
            return RedirectToAction("BlogDetails", new { id = articleId });
        }
    }
}
