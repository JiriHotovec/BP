using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Application.UseCases.Common;
using Hotovec.Orders.Domain.Orders;
using ApplicationException = Hotovec.Orders.Application.Exceptions.ApplicationException;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IOrderEntityRepository _repository;
    private readonly ICreateOrderCommandFactory _factory;

    public CreateOrderCommandHandler(IOrderEntityRepository repository, ICreateOrderCommandFactory factory)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    
    public async Task ExecuteAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        if (await _repository.ExistsAsync(new OrderNumber(command.OrderNumber.Value), cancellationToken))
        {
            throw new ApplicationException("Unable to create order. Order with the same number already exists.");
        }
        
        var entity = _factory.Create(command);
        await _repository.CreateOrUpdateAsync(entity, cancellationToken);
    }
}
