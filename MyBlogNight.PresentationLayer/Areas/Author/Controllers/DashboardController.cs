using Microsoft.AspNetCore.Mvc;

namespace MyBlogNight.PresentationLayer.Areas.Author.Controllers
{
    [Area("Author")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialDashboardHead()
        {
            return PartialView();
        }
        public PartialViewResult PartialDashboardScripts()
        {
            return PartialView();
        }
        public PartialViewResult PartialDashboardFooter()
        {
            return PartialView();
        }
        public PartialViewResult PartialDashboardSidebar()
        {
            return PartialView();
        }
        public PartialViewResult PartialDashboardNavHeader()
        {
            return PartialView();
        }
    }
}
