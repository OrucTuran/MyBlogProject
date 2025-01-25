using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.DtoLayer.Dtos.CommentDtos;
using MyBlogNight.EntityLayer.Concrete;
using MyBlogNight.PresentationLayer.Models;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultWriteAComment : ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public _DefaultWriteAComment(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int articleId)
        {
            var model = new CommentViewDTO();

            // Yorumları al
            model.Comments = _commentService.TGetAll()
                                            .Where(c => c.ArticleId == articleId && c.Status)
                                            .OrderByDescending(c => c.CreatedDate) // Yorumları sırala
                                            .ToList();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                model.UserName = $"{user.Name} {user.Surname}";
                model.IsAuthenticated = true;
            }
            else
            {
                model.IsAuthenticated = false;
            }

            model.ArticleId = articleId;

            return View(model);
        }
    }

    //public class CommentViewModel
    //{
    //    public List<Comment> Comments { get; set; }
    //    public string UserName { get; set; }
    //    public bool IsAuthenticated { get; set; }
    //    public int ArticleId { get; set; }
    //}
}
