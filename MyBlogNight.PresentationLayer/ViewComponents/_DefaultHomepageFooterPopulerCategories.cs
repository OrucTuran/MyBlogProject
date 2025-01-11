using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultHomepageFooterPopulerCategories : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _DefaultHomepageFooterPopulerCategories(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var categoriesWithArticleCount = _articleService.TGetCategoriesWithArticleCount().Take(7).ToList();
            return View(categoriesWithArticleCount);
        }
    }
}
