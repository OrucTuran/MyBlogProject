using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.EntityLayer.Concrete;
using PagedList;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly INewsletterService _newsletterService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public DefaultController(IArticleService articleService, INewsletterService newsletterService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _articleService = articleService;
            _newsletterService = newsletterService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MarkediaIndex(int page = 1)
        {
            int pageSize = 6;
            var values = _articleService.TArticleListWithCategoryAndAppUser().ToPagedList(page, pageSize);
            var user = await _userManager.GetUserAsync(User);
            ViewData["UserName"] = user != null ? user.UserName : null;
            return View(values);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(MarkediaIndex));
        }
        public PartialViewResult PartialMarkediaHead()
        {
            return PartialView();
        }
        public PartialViewResult PartialMarkediaScripts()
        {
            return PartialView();
        }
        public async Task<PartialViewResult> PartialMarkediaHeader()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["UserName"] = user != null ? user.UserName : null;
            return PartialView();
        }

        public PartialViewResult PartialMarkediaSubscribe()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult PartialMarkediaSubscribe(Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                _newsletterService.TInsert(newsletter);
                TempData["Success"] = "Abonelik başarıyla kaydedildi.";
                return Json(new { success = true, message = "Abonelik başarılı" });
            }
            else
            {
                TempData["Error"] = "Bir hata oluştu.";
                return Json(new { success = false, message = "Abonelik hatası" });
            }
        }
    }
}
