using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Application.UseCases.Common;
using Hotovec.Orders.Domain.Orders;
using ApplicationException = Hotovec.Orders.Application.Exceptions.ApplicationException;

namespace Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;

public sealed class CreateNewOrderCommandHandler : ICommandHandler<CreateNewOrderCommand>
{
    private readonly IOrderEntityRepository _repository;
    private readonly ICreateNewOrderCommandOrderFactory _factory;

    public CreateNewOrderCommandHandler(IOrderEntityRepository repository, ICreateNewOrderCommandOrderFactory factory)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    
    public async Task ExecuteAsync(CreateNewOrderCommand command, CancellationToken cancellationToken = default)
    {
        if (await _repository.ExistsAsync(new OrderNumber(command.OrderNumber.Value), cancellationToken))
        {
            throw new ApplicationException("Unable to create order. Order with the same number already exists.");
        }
        
        var entity = _factory.Create(command);
        await _repository.CreateOrUpdateAsync(entity, cancellationToken);
    }
}
