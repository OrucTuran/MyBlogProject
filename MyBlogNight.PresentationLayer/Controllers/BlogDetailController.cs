using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IArticleService _articleService;

        public BlogDetailController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult BlogDetails(int id)
        {
            var value = _articleService.TArticleListWithCategoryAndAppUserByArticleId(id);
            return View(value);
        }
    }
}
