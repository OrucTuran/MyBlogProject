using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultHomepageFooterPopularPosts : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _DefaultHomepageFooterPopularPosts(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var articles = _articleService.TGetArticlesByViewCount();
            return View(articles);
        }
    }
}
