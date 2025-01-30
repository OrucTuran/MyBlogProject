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
        public PartialViewResult DashboardPartialHead()
        {
            return PartialView();
        }
    }
}
