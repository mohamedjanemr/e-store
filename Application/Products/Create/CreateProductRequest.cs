using Domain.Abstractions;
using Domain.Products;
using MediatR;

namespace Application.Products.Create;

public class CreateProductRequest: IRequest<CreateProductResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IAppUnitOfWork _appUnitOfWork;

    public CreateProductRequestHandler(IProductRepository productRepository,IAppUnitOfWork appUnitOfWork)
    {
        _productRepository = productRepository;
        _appUnitOfWork = appUnitOfWork;
    }
    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        Product product = new (request.Name,request.Description,request.Price);
        await _productRepository.AddAsync(product,cancellationToken);
        await _appUnitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateProductResponse(product);
    }
}