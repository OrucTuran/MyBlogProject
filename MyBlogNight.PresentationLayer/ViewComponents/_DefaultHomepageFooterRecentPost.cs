using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultHomepageFooterRecentPost : ViewComponent
    {
        private readonly IArticleService _articleService;
        public _DefaultHomepageFooterRecentPost(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _articleService.TArticleListWithCategoryAndAppUser()
                .OrderByDescending(a => a.CreatedDate)
                            .Take(4)
                            .ToList();
            return View(values);
        }
    }
}
