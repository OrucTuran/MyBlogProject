using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;

namespace MyBlogNight.PresentationLayer.ViewComponents
{
    public class _DefaultBlogDetailTagClouds : ViewComponent
    {
        private readonly ITagCloudService _tagCloudService;

        public _DefaultBlogDetailTagClouds(ITagCloudService tagCloudService)
        {
            _tagCloudService = tagCloudService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _tagCloudService.TGetAll();
            return View(values);
        }
    }
}
