namespace MyBlogNight.PresentationLayer.Models
{
    public class NewBlogViewModel
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public int CategoryId { get; set; }
        public IFormFile CoverImage { get; set; }
    }
}
