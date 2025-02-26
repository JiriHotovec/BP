namespace Hotovec.Orders.Application.UseCases.Common;

public interface ICommandHandler<in T>
{
    Task ExecuteAsync(T command, CancellationToken cancellationToken = default);
}