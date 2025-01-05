using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IArticleService _articleService;

        public DefaultController(IArticleService articleService)
        {
            _articleService = articleService;
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
