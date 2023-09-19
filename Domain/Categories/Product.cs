using Domain.Products;

namespace Domain.Categories;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Product> Products { get; set; }
}
