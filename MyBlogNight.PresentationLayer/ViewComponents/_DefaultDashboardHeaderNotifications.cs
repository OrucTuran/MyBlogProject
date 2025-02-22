using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultDashboardHeaderNotifications : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _DefaultDashboardHeaderNotifications(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _commentService.TGetAll().OrderByDescending(x => x.CreatedDate).ToList();
            return View(values);
        }
    }
}
