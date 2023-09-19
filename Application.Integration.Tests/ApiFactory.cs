using System.Text.Json;
using System.Text.Json.Serialization;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using EStore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Integration.Tests;

public class ApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MsSqlTestcontainer _mssqlContainer;

    public ApiFactory()
    {
        _mssqlContainer = new ContainerBuilder<MsSqlTestcontainer>()
            .WithName($"refund_sys_test-{Guid.NewGuid()}")
            .WithDatabase(new MsSqlTestcontainerConfiguration()
            {
                Password = "456000Moh@",
                Database = $"e_store_{Guid.NewGuid()}"
            })
            .Build();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration((configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:MSSqlConnection",
                    $"{_mssqlContainer.ConnectionString};TrustServerCertificate=True"
                }
            });
        });
        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging => logging.ClearProviders());
        builder.ConfigureTestServices(services =>
        {
            services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        });
    }

    public async Task InitializeAsync()
    {
        if (_mssqlContainer is not null)
            await _mssqlContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _mssqlContainer.StopAsync();
        foreach (var factory in this.Factories)
            await factory.DisposeAsync();
    }
}