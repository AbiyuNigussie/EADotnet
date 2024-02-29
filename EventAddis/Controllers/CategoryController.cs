using AutoMapper;
using EventAddis.Entity;
using EventAddis.Models;
using EventAddis.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAddis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetCategories();

            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult CreateCategory(CreateCategory categoryCreate)
        {

            if (_categoryService.GetCategories().Where(c => c.Name.TrimEnd().ToLower() == categoryCreate.Name.Trim().ToLower()).Any())
            {
                ModelState.AddModelError("", "Category Already Exist!");
                return StatusCode(422, ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryService.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Creating Category!");
                return StatusCode(500, ModelState);
            }
            return Ok("Category Successfully Created!");

        }
    }
}
