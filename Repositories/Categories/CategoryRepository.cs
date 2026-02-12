using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext context) :
        GenericRepository<Category>(context), ICategoryRepository
    {
        public IQueryable<Category> GetCategoryWithProductsAsync()
        {
            return Context.Categories.Include(c => c.Products).AsQueryable();
        }

        public Task<Category?> GetCategoryWithProductsAsync(int id)
        {
           return Context.Categories.Include(c => c.Products)//eager loading kismi
                .FirstOrDefaultAsync(c => c.Id == id);
        } 
    }
}
