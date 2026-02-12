using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;

namespace App.Repositories.Categories
{
    public   class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<Product>? Products { get; set; } = new List<Product>();
    }
}
