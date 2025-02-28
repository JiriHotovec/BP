using Hotovec.Orders.Application.Persistence;
using Hotovec.Orders.Application.UseCases.Common;

namespace Hotovec.Orders.Application.UseCases.Commands.DeleteOrder;

public sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderEntityRepository _repository;

    public DeleteOrderCommandHandler(IOrderEntityRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<bool> ExecuteAsync(DeleteOrderCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        return await _repository.DeleteAsync(command.OrderNumber, cancellationToken);
    }
}
