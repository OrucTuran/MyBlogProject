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
        private readonly ICategoryService _categoryService;
        public DefaultController(IArticleService articleService, INewsletterService newsletterService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _newsletterService = newsletterService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MarkediaIndex(int page = 1)
        {
            int pageSize = 6;
            var values = _articleService.TArticleListWithCategoryAndAppUser().ToPagedList(page,pageSize);
            return View(values);
        }

        public PartialViewResult PartialMarkediaHead()
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
    }
}
