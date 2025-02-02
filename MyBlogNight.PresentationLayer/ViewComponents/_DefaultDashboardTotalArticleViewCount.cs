using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardTotalArticleViewCount : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _DefaultDashboardTotalArticleViewCount(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var value = _articleService.TGetTotalArticleViewCount();
            return View(value);
        }
    }
}
