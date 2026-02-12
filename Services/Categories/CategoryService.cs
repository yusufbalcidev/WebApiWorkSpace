using System.Net;
using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, UnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResult<CategoryWithProductsDto>> GetAllWithProductsAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if (category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category not found", HttpStatusCode.NotFound);
            }
            var categoriesAsDto = mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(categoriesAsDto);
        }
        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetAllWithProductsAsync()
        {
            var categories = await categoryRepository.GetCategoryWithProductsAsync().ToListAsync();
            var categoriesAsDto = mapper.Map<List<CategoryWithProductsDto>>(categories);
            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoriesAsDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();
            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
        }
        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found", HttpStatusCode.NotFound);
            }
            var categoryAsDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult<int>.Fail("Category already exists",
                    HttpStatusCode.NotFound);
            }
            var newCategory = new Category { Name = request.Name };
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<int>.Success(newCategory.Id);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
            }
            var isCategoryNameExist = await categoryRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();
            if (isCategoryNameExist)
            {
                return ServiceResult.Fail("Category name already exists", HttpStatusCode.BadRequest);
            }
            category = mapper.Map(request, category);
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
            }
            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        
    }
}