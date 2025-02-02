using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DtoLayer.Dtos.DashboardDtos;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardArticleCategoryCommentAuthorCount : ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IAppUserService _appUserService;
        private readonly ICommentService _commentService;

        public _DefaultDashboardArticleCategoryCommentAuthorCount(
            IArticleService articleService,
            ICategoryService categoryService,
            IAppUserService appUserService,
            ICommentService commentService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _appUserService = appUserService;
            _commentService = commentService;
        }
        public IViewComponentResult Invoke()
        {
            var dto = new ArticleCategoryCommentAuthorCountDTO
            {
                TotalArticleCount = _articleService.TGetAll().Count(),
                TotalCategoryCount = _categoryService.TGetAll().Count(),
                TotalCommentCount = _commentService.TGetAll().Count(),
                TotalAuthorCount = _appUserService.TGetAll().Count(x => x.isAuthor)
            };
            return View(dto);
        }
    }
}
