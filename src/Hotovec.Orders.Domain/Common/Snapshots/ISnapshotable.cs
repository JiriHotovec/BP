namespace Hotovec.Orders.Domain.Common.Snapshots;

public interface ISnapshotable<out T>
{
    T ToSnapshot();
}
