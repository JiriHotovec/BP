using Hotovec.Orders.Domain.Common.Snapshots;

namespace Hotovec.Orders.Domain.Common.Entities;

/// <summary>
/// Represents an abstract base class for entities with a uniquely identifiable ID and snapshot capability.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the entity. Must implement <see cref="IEquatable{T}"/>.</typeparam>
/// <typeparam name="TSnapshot">The type representing the snapshot state of the entity. Must be a reference type.</typeparam>
public abstract class Entity<TId, TSnapshot> : ISnapshotable<TSnapshot> 
    where TId : IEquatable<TId>
    where TSnapshot : class
{
    protected Entity(TId id)
    {
        Id = id;
    }
    
    /// Gets the unique identifier for the entity.
    /// This identifier is used to determine the uniqueness of the entity instance.
    /// The value is assigned during the creation of the entity and is immutable.
    public TId Id { get; }

    /// Converts the current entity or object into its snapshot representation.
    /// <returns>Returns a snapshot of the current state of the entity or object.</returns>
    public abstract TSnapshot ToSnapshot();
}
