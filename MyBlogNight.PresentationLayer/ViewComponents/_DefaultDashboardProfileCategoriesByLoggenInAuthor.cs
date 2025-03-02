using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardProfileCategoriesByLoggenInAuthor : ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<AppUser> _userManager;

        public _DefaultDashboardProfileCategoriesByLoggenInAuthor(IArticleService articleService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userValue = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _articleService.TGetArticlesByAppUserId(userValue.Id).OrderByDescending(x => x.CreatedDate).ToList();
            return View(values);
        }
    }
}
