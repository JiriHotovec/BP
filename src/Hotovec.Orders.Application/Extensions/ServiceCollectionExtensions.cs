using Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;
using Hotovec.Orders.Application.UseCases.Common;
using Hotovec.Orders.Application.UseCases.Queries.GetOrderById;
using Hotovec.Orders.Domain.Orders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hotovec.Orders.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.TryAddSingleton(TimeProvider.System);
        services.TryAddScoped<IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>, GetOrderByIdQueryHandler>();
        services.TryAddScoped<ICommandHandler<CreateOrderCommand>, CreateOrderCommandHandler>();
        services.TryAddSingleton<ICreateOrderCommandFactory, CreateOrderCommandFactory>();

        return services;
    }
}
