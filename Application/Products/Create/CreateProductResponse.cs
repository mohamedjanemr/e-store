using Domain.Products;

namespace Application.Products.Create;

public class CreateProductResponse
{
    public CreateProductResponse()
    {
        
    }

    public CreateProductResponse(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

}