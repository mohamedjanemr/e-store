using Domain.Products;

namespace Persistence.Products;

public class ProductRepository:IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Product product, CancellationToken cancellationToken )
    {
       await _context.Products.AddAsync(product,cancellationToken);
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}