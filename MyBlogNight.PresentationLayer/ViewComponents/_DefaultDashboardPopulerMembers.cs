using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardPopulerMembers : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _DefaultDashboardPopulerMembers(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke()
        {
            var activeUsers = _commentService.TGetMostActiveUsers();
            return View(activeUsers);
        }
    }
}
