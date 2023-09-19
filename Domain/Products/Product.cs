namespace Domain.Products;

public class Product
{
    private Product()
    {
        Id = Guid.NewGuid();
        CreateAt = DateTime.Now;
    }

    public Product(string name, string description, decimal price):this()
    {
        Name = name;
        Description = description;
        Price = price;
    }
    
    public Guid Id { get;private set; }
    public string Name { get;private set; }
    public string Description { get;private set; }
    public decimal Price { get;private set; }
    public DateTime CreateAt { get;private set; }
    public DateTime? UpdateAt { get;private set; }
    
    
    public void SetName(string name)
    {
        Name = name;
        UpdateAt = DateTime.Now;
    }
}