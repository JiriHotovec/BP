using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.Persistence;

public interface IOrderEntityRepository
{
    Task CreateOrUpdateAsync(OrderEntity order, CancellationToken cancellationToken = default);

    Task<OrderEntity?> TryGetAsync(OrderNumber identity, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<OrderEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<bool> ExistsAsync(OrderNumber identity, CancellationToken cancellationToken = default);
    
    Task<bool> DeleteAsync(OrderNumber identity, CancellationToken cancellationToken = default);
}
