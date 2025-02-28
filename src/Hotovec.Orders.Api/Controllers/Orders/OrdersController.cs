using Hotovec.Orders.Api.Controllers.Orders.CreateOrder;
using Hotovec.Orders.Api.Controllers.Orders.DeleteOrder;
using Hotovec.Orders.Api.Controllers.Orders.GetAllOrders;
using Hotovec.Orders.Api.Controllers.Orders.GetOrderById;
using Hotovec.Orders.Api.Extensions;
using Hotovec.Orders.Application.UseCases.Commands.CreateOrder;
using Hotovec.Orders.Application.UseCases.Commands.DeleteOrder;
using Hotovec.Orders.Application.UseCases.Common;
using Hotovec.Orders.Application.UseCases.Queries.GetAllOrders;
using Hotovec.Orders.Application.UseCases.Queries.GetOrderById;
using Microsoft.AspNetCore.Mvc;

namespace Hotovec.Orders.Api.Controllers.Orders;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class OrdersController(
    IQueryHandler<GetAllOrdersQuery, GetAllOrdersResult> _getAllOrdersQueryHandler,
    IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult> _getOrderByIdQueryHandler,
    ICommandHandler<CreateOrderCommand> _createOrderCommandHandler,
    ICommandHandler<DeleteOrderCommand, bool> _deleteOrderCommandHandler,
    ILogger<OrdersController> _logger)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetAllOrdersResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all orders.");

        var result = await _getAllOrdersQueryHandler
            .ExecuteAsync(new GetAllOrdersQuery(), cancellationToken);
        
        return Ok(result.AsResponse());
    }

    [HttpGet("{OrderNumber}")]
    [ProducesResponseType(typeof(GetOrderByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(
        [FromRoute] GetOrderByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        _logger.LogInformation("Getting order [{OrderNumber}].", request.OrderNumber);

        var result = await _getOrderByIdQueryHandler
            .ExecuteAsync(request.AsQuery(), cancellationToken);
        
        return result.Order != null
            ? Ok(result.AsResponse())
            : NotFound();
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(GetOrderByIdResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        _logger.LogInformation("Creating order [{Order}].", request);
        
        await _createOrderCommandHandler
            .ExecuteAsync(request.AsCommand(), cancellationToken);

        return CreatedAtAction(nameof(GetOrderById), new { OrderNumber = request.OrderNumber }, null);
    }
    
    [HttpDelete("{OrderNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(
        [FromRoute] DeleteOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        _logger.LogInformation("Deleting order [{OrderNumber}].", request.OrderNumber);
        
        var result = await _deleteOrderCommandHandler
            .ExecuteAsync(request.AsCommand(), cancellationToken);

        return result ? Ok() : NotFound();
    }
}
