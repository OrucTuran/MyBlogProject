using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.EntityLayer.Concrete;
using MyBlogNight.PresentationLayer.Models;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
            if (result.Succeeded)
            {
                return RedirectToAction("MarkediaIndex", "Default");
            }
            else
            {
                return View();
            }
        }
        public IActionResult AuthorIndex()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AuthorIndex(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username); // Kullanıcıyı bul
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                ViewBag.abc = "Kullanıcı bulunamadı.";
                return View();
            }

            if (!user.isAuthor)
            {
                ModelState.AddModelError("", "Bu kullanıcı yazar değil!");
                ViewBag.abc = "Bu kullanıcı yazar değil!";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Author" });
            }

            ModelState.AddModelError("", "Giriş başarısız.");
            return View();
        }
    }
}
/*
//50 Derste Mvc
//50 Derste Tatil Seyahat Sitesi
//Mvc5 Admin Panelli Cv Sitesi
//Mvc Proje Kampı
//AspNet Core ile Adım Adım Web Geliştirme
*/