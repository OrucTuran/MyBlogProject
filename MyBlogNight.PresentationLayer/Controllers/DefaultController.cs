using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IArticleService _articleService;
       private readonly INewsletterService _newsletterService;
        public DefaultController(IArticleService articleService, INewsletterService newsletterService)
        {
            _articleService = articleService;
            _newsletterService = newsletterService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MarkediaIndex()
        {
            var values = _articleService.TArticleListWithCategoryAndAppUser();
            return View(values);
        }
        public PartialViewResult PartialMarkediaHead()
        {
            return PartialView();
        }
        public PartialViewResult PartialMarkediaSidebar()
        {
            return PartialView();
        }
        public PartialViewResult PartialMarkediaScripts()
        {
            return PartialView();
        }
        public PartialViewResult PartialMarkediaHeader()
        {
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
                _newsletterService.TInsert(newsletter); // Abone ekleme işlemi
                TempData["Success"] = "Abonelik başarıyla kaydedildi.";
                return Json(new { success = true, message = "Abonelik başarılı" }); // Başarı mesajı döndür
            }
            else
            {
                TempData["Error"] = "Bir hata oluştu.";
                return Json(new { success = false, message = "Abonelik hatası" }); // Hata mesajı döndür
            }
        }

        public PartialViewResult PartialMarkediaFooterRecentPosts()
        {
            return PartialView();
        }
        public PartialViewResult PartialMarkediaFooterPopularPosts()
        {
            return PartialView();
        }
        public PartialViewResult PartialMarkediaFooterPopularCategories()
        {
            return PartialView();
        }
    }
}
