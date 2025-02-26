using Hotovec.Orders.Application.Persistance;
using Hotovec.Orders.Application.UseCases.Common;
using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderEntity?>
{
    private readonly IOrderEntityRepository _repository;

    public GetOrderByIdQueryHandler(IOrderEntityRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<OrderEntity?> ExecuteAsync(GetOrderByIdQuery input, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        
        return await _repository.TryGetAsync(input.OrderNumber, cancellationToken);
    }
}
