using Application.Products.Create;
using Domain.Abstractions;
using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(new[] { typeof(CreateProductRequest).Assembly });
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

    var connection = builder.Configuration.GetConnectionString("MSSqlConnection");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(connection, m =>
        m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
);


var app = builder.Build();
using var scope = app.Services.CreateScope();

var context=  scope.ServiceProvider.GetRequiredService<AppDbContext>();
await context.Database.MigrateAsync();

app.MapGet("/", () => "Hello World!");
app.MapPost("/api/products", async (
    CreateProductRequest request,
    ISender sender
) =>
{
    var result = await sender.Send(request);
    return Results.Ok(result);
});

app.Run();