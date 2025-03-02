using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Application.UseCases.Common;

namespace Hotovec.Orders.Application.UseCases.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, GetOrderByIdQueryResult>
{
    private readonly IOrderEntityRepository _repository;

    public GetOrderByIdQueryHandler(IOrderEntityRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<GetOrderByIdQueryResult> ExecuteAsync(GetOrderByIdQuery input, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        
        var order = await _repository.TryGetAsync(input.OrderNumber, cancellationToken);
        
        return new GetOrderByIdQueryResult(order);
    }
}
