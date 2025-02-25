using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardProfileBlogOfTheDayByLoggedInAuthor : ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<AppUser> _userManager;

        public _DefaultDashboardProfileBlogOfTheDayByLoggedInAuthor(IArticleService articleService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity == null || string.IsNullOrEmpty(User.Identity.Name))
            {
                return View(new List<Article>());
            }

            var userValue = await _userManager.FindByNameAsync(User.Identity.Name);
            if (userValue == null)
            {
                return View(new List<Article>());
            }

            var values = await _articleService.TGetArticlesByAppUserIdAsync(userValue.Id);
            return View(values);
        }


    }
}
