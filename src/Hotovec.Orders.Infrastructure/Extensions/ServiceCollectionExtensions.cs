using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Infrastructure.Persistence.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hotovec.Orders.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.TryAddSingleton<IMongoConnection, MongoConnection>();
        services.TryAddSingleton<IOrderEntityRepository, MongoDbOrderEntityRepository>();
        services.Configure<MongoOptions>(configuration.GetSection("MongoDb"));

        return services;
    }
}
