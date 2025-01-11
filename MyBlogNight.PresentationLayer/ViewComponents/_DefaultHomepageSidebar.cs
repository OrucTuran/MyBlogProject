using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultHomepageSidebar : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _DefaultHomepageSidebar(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _articleService.TArticleListWithCategoryAndAppUser()
                .OrderByDescending(a => a.CreatedDate)
                            .Take(3)
                            .ToList();
            return View(values);
        }
    }
}
