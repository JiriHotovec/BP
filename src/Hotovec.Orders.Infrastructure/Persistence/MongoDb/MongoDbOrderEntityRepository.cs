using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Domain.Orders;
using Hotovec.Orders.Domain.Orders.Snapshots;
using MongoDB.Driver;

namespace Hotovec.Orders.Infrastructure.Persistence.MongoDb;

internal sealed class MongoDbOrderEntityRepository : IOrderEntityRepository
{
    private static readonly string CollectionName = "orders";

    private readonly IMongoCollection<OrderSnapshot> _collection;

    public MongoDbOrderEntityRepository(IMongoConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);
        
        _collection = connection.Database.GetCollection<OrderSnapshot>(CollectionName);
    }

    public async Task CreateOrUpdateAsync(OrderEntity order, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(order);

        var snapshot = order.ToSnapshot();
        await _collection.ReplaceOneAsync(i => i.Id == order.Id.ToString(),
            snapshot,
            new ReplaceOptions { IsUpsert = true },
            cancellationToken);
    }

    public async Task<OrderEntity?> TryGetAsync(OrderNumber identity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(identity);
        
        using var cursor =
            await _collection.FindAsync(i => i.Id == identity.ToString(), cancellationToken: cancellationToken);
        var snapshot = await cursor.FirstOrDefaultAsync(cancellationToken);
        
        return snapshot is null ? null : new OrderEntity(snapshot);
    }

    public async Task<bool> ExistsAsync(OrderNumber identity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(identity);

        using var cursor =
            await _collection.FindAsync(i => i.Id == identity.ToString(), cancellationToken: cancellationToken);
        var snapshot = await cursor.FirstOrDefaultAsync(cancellationToken);
        
        return snapshot is not null;
    }
}
