using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.Areas.Author.Controllers
{
    [Area("Author")]
    public class DashboardController : Controller
    {
        private readonly IArticleService _articleService;

        public DashboardController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DashboardBarChart()
        {
            var blogOverView = _articleService.TGetBlogOverview();

            if (blogOverView == null || !blogOverView.Any())
            {
                return Json(new { success = false, message = "Veri bulunamadı" });
            }

            return Json(blogOverView);
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
