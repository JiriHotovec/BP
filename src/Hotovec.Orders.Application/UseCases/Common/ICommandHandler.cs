namespace Hotovec.Orders.Application.UseCases.Common;

public interface ICommandHandler<in T>
{
    Task ExecuteAsync(T command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TInput, TOutput>
{
    Task<TOutput> ExecuteAsync(TInput command, CancellationToken cancellationToken = default);
}
