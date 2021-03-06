using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeBusters.Services.Category;
using Microsoft.AspNetCore.Authorization;
using CodeBusters.Models.Category;

namespace CodeBusters.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var category = await _categoryService.GetAllCategoryAsync();
            return Ok(category);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _categoryService.CreateCategoryAsync(request);
            if (registerResult)
            {
                return Ok("Category created successfully.");
            }

            return BadRequest("Category could not be created.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryById([FromBody] CategoryUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _categoryService.UpdateCategoryAsync(request)
                ? Ok("Category updated successfully.")
                : BadRequest("Category could not be updated.");
        }
        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            return await _categoryService.DeleteCategoryAsync(categoryId)
                ? Ok($"Category {categoryId} was deleted successfully.")
                : BadRequest($"Category {categoryId} could not be deleted.");
        }
    }
}