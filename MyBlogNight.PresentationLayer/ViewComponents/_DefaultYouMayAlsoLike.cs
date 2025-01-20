using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultYouMayAlsoLike : ViewComponent
    {
        private readonly IArticleService _articleService;
        public _DefaultYouMayAlsoLike(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _articleService.TGetRandomTwoTop5ViewedArticles().ToList();
            return View(values);
        }
    }
}
