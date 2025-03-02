using Microsoft.AspNetCore.Mvc;
using MyBlogNight.BusinessLayer.Abstract;
using MyBlogNight.BusinessLayer.ValidationRules.CategoryValidationRules;
using MyBlogNight.EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;

namespace MyBlogNight.PresentationLayer.Areas.Author.Controllers
{
    [Area("Author")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult CategoryList(int page = 1)
        {
            int pageSize = 7;
            var values = _categoryService.TGetAll().ToPagedList(page,pageSize);
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            ModelState.Clear(); //mevcuttaki butun data annotacionslari siler. bunun yerine asagidaki benim kurallarim gecerli olur.
            CreateCategoryValidator validationRules = new CreateCategoryValidator();
            ValidationResult result = validationRules.Validate(category); //category den gelen degerleri validate et
            if (result.IsValid) //validasyona takilmiyorsa
            {
                _categoryService.TInsert(category);
                return RedirectToAction(nameof(CategoryList));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName/*hataya sebep olan property*/, item.ErrorMessage);
                }
                return View();
            }

        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryService.TDelete(id);
            return RedirectToAction(nameof(CategoryList));
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _categoryService.TGetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.TUpdate(category);
            return RedirectToAction(nameof(CategoryList));
        }
    }
}