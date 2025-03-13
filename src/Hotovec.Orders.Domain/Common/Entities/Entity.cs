using Hotovec.Orders.Domain.Common.Snapshots;

namespace Hotovec.Orders.Domain.Common.Entities;

public abstract class Entity<TId, TSnapshot> : ISnapshotable<TSnapshot> 
    where TId : IEquatable<TId>
    where TSnapshot : class
{
    protected Entity(TId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        Id = id;
    }
    
    public TId Id { get; }

    public abstract TSnapshot ToSnapshot();
}
