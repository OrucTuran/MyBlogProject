using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardProfilAddingNewPostByLoggedInAuthor : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _DefaultDashboardProfilAddingNewPostByLoggedInAuthor(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _categoryService.TGetAll();

            return View(categories);
        }
    }
}
