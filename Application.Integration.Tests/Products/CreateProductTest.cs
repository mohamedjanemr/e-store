using System.Net.Http.Json;
using Application.Products.Create;
using FluentAssertions;

namespace Application.Integration.Tests.Products;

public class CreateProductTest:IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public CreateProductTest(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ShouldBeCreated_WhenValidRequest()
    {
        var command = new CreateProductRequest()
        {
            Description = "Test",
            Name = "Test",
            Price = 100
        };
        
        var response= await _client.PostAsJsonAsync("/api/products",command);
        var createProductResponse = await response.Content.ReadFromJsonAsync<CreateProductResponse>();
        createProductResponse.Id.Should().NotBe(Guid.Empty);
    }

}