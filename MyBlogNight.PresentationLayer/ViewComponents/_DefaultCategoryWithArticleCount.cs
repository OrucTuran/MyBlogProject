
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultCategoryWithArticleCount : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _DefaultCategoryWithArticleCount(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var categoriesWithArticleCount = _articleService.TGetCategoriesWithArticleCount();
            return View(categoriesWithArticleCount);
        }
    }
}
