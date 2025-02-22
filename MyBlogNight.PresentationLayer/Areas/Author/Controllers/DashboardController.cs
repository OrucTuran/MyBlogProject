using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.Areas.Author.Controllers
{
    [Area("Author")]
    public class DashboardController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly SignInManager<AppUser> _signInManager;

        public DashboardController(IArticleService articleService, SignInManager<AppUser> signInManager)
        {
            _articleService = articleService;
            _signInManager = signInManager;
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
        public IActionResult DashboardPieChartByAuthor(int authorId)
        {
            var blogOverviewByAuthor = _articleService.TGetBlogOverviewByAuthor(authorId);

            if (blogOverviewByAuthor == null || !blogOverviewByAuthor.Any())
            {
                return Json(new { success = false, message = "Veri Bulunamadı" });
            }
            return Json(blogOverviewByAuthor);
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
