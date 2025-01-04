using Microsoft.AspNetCore.Mvc;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MarkediaIndex()
        {
            return View();
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
