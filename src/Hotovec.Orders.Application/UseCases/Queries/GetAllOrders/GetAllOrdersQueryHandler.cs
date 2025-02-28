using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Application.UseCases.Common;

namespace Hotovec.Orders.Application.UseCases.Queries.GetAllOrders;

public sealed class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, GetAllOrdersResult>
{
    private readonly IOrderEntityRepository _repository;

    public GetAllOrdersQueryHandler(IOrderEntityRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<GetAllOrdersResult> ExecuteAsync(GetAllOrdersQuery input, CancellationToken cancellationToken = default)
    {
        var orders = await _repository.GetAllAsync(cancellationToken);
        
        return new GetAllOrdersResult(orders.ToList());
    }
}
