using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultBlogDetailsComments : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _DefaultBlogDetailsComments(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _commentService.TGetAll();
            return View(values);
        }
    }
}
