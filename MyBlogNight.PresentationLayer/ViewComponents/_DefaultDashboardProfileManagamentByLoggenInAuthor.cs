using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardProfileManagamentByLoggenInAuthor : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _DefaultDashboardProfileManagamentByLoggenInAuthor(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User?.Identity == null || string.IsNullOrEmpty(User.Identity.Name))
            {
                return View(new AppUser());
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return View(new AppUser());
            }

            return View(user);
        }
    }
}
