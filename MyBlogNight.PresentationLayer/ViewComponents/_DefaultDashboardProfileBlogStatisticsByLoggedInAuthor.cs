using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardProfileBlogStatisticsByLoggedInAuthor : ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<AppUser> _userManager;

        public _DefaultDashboardProfileBlogStatisticsByLoggedInAuthor(IArticleService articleService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userValue = _userManager.FindByNameAsync(User.Identity.Name);
            var values = _articleService.TGetDashboardProfileStatisticsByAuthor(userValue.Id);
            return View(values);
        }
    }
}