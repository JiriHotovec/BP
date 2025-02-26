namespace Hotovec.Orders.Application.UseCases.Common;

public interface IQueryHandler<in TInput,TOutput>
{
    Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}
