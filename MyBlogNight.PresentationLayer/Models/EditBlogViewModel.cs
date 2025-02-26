using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlogNight.EntityLayer.Concrete;

namespace MyBlogNight.PresentationLayer.Models
{
    public class EditBlogViewModel
    {
        public int ArticleId { get; set; } 
        public string Title { get; set; } 
        public string Detail { get; set; } 
        public int CategoryId { get; set; }
        public string? CoverImageUrl { get; set; }
        public IFormFile CoverImage { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
