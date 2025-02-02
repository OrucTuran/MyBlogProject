using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardSelectiveComments : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _DefaultDashboardSelectiveComments(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _commentService.TGetAll().Take(5).ToList();
            return View(values);
        }
    }
}
