using Hotovec.Orders.Api;
using Hotovec.Orders.Infrastructure.Persistence.MongoDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Testcontainers.MongoDb;

namespace Hotovec.Orders.Integration.Test.UseCases;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer;
    
    public CustomWebApplicationFactory()
    {
        _mongoDbContainer = new MongoDbBuilder()
            .WithHostname("MongoDbTests")
            .WithImage("mongo:6.0")
            .WithUsername("root")
            .WithPassword("crypto_password")
            .WithExposedPort(27017)
            .WithName("OrdersTests")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = _mongoDbContainer.GetConnectionString(); // !!
        builder.ConfigureTestServices(s =>
        {
            ServiceCollectionDescriptorExtensions.RemoveAll(s, typeof(IOptions<MongoOptions>));
            var mongoOptions = new MongoOptions()
            {
                ConnectionString = connectionString,
                DatabaseName = "HOTOVEC",
                CollectionName = "ORDERS"
            };
            ServiceCollectionServiceExtensions.AddSingleton<IOptions<MongoOptions>>(s, _ => Options.Create(mongoOptions));
        });
    }

    public async Task InitializeAsync()
    {
        await _mongoDbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _mongoDbContainer.DisposeAsync();
    }
}
