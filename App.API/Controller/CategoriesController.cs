using App.API.Controllers;
using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet] // GET api/Categories
        public async Task<IActionResult> GetCategories()
            => CreateActionResult(await categoryService.GetAllAsync());

        [HttpGet("{id}")] // ✅ GET api/Categories/5
        public async Task<IActionResult> GetCategory(int id)
            => CreateActionResult(await categoryService.GetByIdAsync(id));

        [HttpGet("products")] // GET api/Categories/products
        public async Task<IActionResult> GetCategoriesWithProducts()
            => CreateActionResult(await categoryService.GetAllWithProductsAsync());

        [HttpGet("{id}/products")] // GET api/Categories/5/products
        public async Task<IActionResult> GetCategoryWithProducts(int id)
            => CreateActionResult(await categoryService.GetAllWithProductsAsync(id));

        [HttpPost] // POST api/Categories
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
            => CreateActionResult(await categoryService.CreateAsync(request));

        [HttpPut("{id}")] // PUT api/Categories/5
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request)
            => CreateActionResult(await categoryService.UpdateAsync(id, request));

        [HttpDelete("{id}")] // DELETE api/Categories/5
        public async Task<IActionResult> DeleteCategory(int id)
            => CreateActionResult(await categoryService.DeleteAsync(id));
    }
}