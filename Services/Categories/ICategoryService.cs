using System.Net;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;

namespace App.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CategoryWithProductsDto>> GetAllWithProductsAsync(int categoryId);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetAllWithProductsAsync();
        Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id,UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
