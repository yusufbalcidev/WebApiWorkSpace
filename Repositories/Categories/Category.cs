using App.Repositories.Products;

namespace App.Repositories.Categories
{
    public class Category:IAuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<Product>? Products { get; set; } = new List<Product>();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
