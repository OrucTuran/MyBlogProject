using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardBlogOverviewGraph : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _DefaultDashboardBlogOverviewGraph(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var blogOverView = _articleService.TGetBlogOverview();
            return View(blogOverView);
        }
    }
}
