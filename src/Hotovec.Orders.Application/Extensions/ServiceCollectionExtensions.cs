using Hotovec.Orders.Application.UseCases.Commands.CreateOrder;
using Hotovec.Orders.Application.UseCases.Commands.DeleteOrder;
using Hotovec.Orders.Application.UseCases.Common;
using Hotovec.Orders.Application.UseCases.Queries.GetOrderById;
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
        services.TryAddScoped<ICommandHandler<DeleteOrderCommand, bool>, DeleteOrderCommandHandler>();
        services.TryAddSingleton<ICreateOrderCommandFactory, CreateOrderCommandFactory>();

        return services;
    }
}
