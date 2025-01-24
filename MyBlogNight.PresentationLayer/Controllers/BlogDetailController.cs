using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DataAccessLayer.Context;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;

        public BlogDetailController(IArticleService articleService, ICommentService commentService)
        {
            _articleService = articleService;
            _commentService = commentService;
        }

        public IActionResult BlogDetails(int id)
        {
            var value = _articleService.TArticleListWithCategoryAndAppUserByArticleId(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment, int articleId)
        {
            comment.CreatedDate = DateTime.Now;
            comment.Status = true;

            comment.ArticleId = articleId;

            _commentService.TInsert(comment);
            return RedirectToAction("BlogDetails", new { id = articleId });
        }
    }
}
