using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.Persistence;

/// <summary>
/// Represents the repository interface for managing and accessing <see cref="OrderEntity"/> instances.
/// </summary>
public interface IOrderEntityRepository
{
    /// Asynchronously creates a new order or updates an existing one in the repository.
    /// <param name="order">The order entity to create or update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateOrUpdateAsync(OrderEntity order, CancellationToken cancellationToken = default);

    /// Attempts to find and retrieve an order entity by its unique identifier.
    /// <param name="identity">
    /// The unique identifier (GUID) of the order entity to retrieve.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the operation.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the order entity if found; otherwise, null.
    /// </returns>
    Task<OrderEntity?> TryGetAsync(OrderNumber identity, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<OrderEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<bool> ExistsAsync(OrderNumber identity, CancellationToken cancellationToken = default);
    
    Task<bool> DeleteAsync(OrderNumber identity, CancellationToken cancellationToken = default);
}
