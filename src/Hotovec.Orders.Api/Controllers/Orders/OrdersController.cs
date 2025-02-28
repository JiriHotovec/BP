using Hotovec.Orders.Api.Controllers.Orders.CreateOrder;
using Hotovec.Orders.Api.Controllers.Orders.GetOrderById;
using Hotovec.Orders.Api.Extensions;
using Hotovec.Orders.Application.UseCases.Commands.CreateNewOrder;
using Hotovec.Orders.Application.UseCases.Common;
using Hotovec.Orders.Application.UseCases.Queries.GetOrderById;
using Microsoft.AspNetCore.Mvc;

namespace Hotovec.Orders.Api.Controllers.Orders;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class OrdersController(
    IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult> _getOrderByIdQueryHandler,
    ICommandHandler<CreateOrderCommand> _createOrderCommandHandler,
    ILogger<OrdersController> _logger)
    : ControllerBase
{
    private readonly ILogger<OrdersController> _logger = _logger ?? throw new ArgumentNullException(nameof(_logger));

    [HttpGet("{OrderNumber}")]
    [ProducesResponseType(typeof(GetOrderByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(
        [FromRoute] GetOrderByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        _logger.LogInformation("Getting order [{OrderNumber}]", request.OrderNumber);

        var result = await _getOrderByIdQueryHandler
            .ExecuteAsync(request.AsQuery(), cancellationToken);
        
        return result.Order != null
            ? Ok(result.AsResponse())
            : NotFound();
    }
    
    [HttpPost("Create")]
    [ProducesResponseType(typeof(GetOrderByIdResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        _logger.LogInformation("Creating order [{Order}]", request);
        
        await _createOrderCommandHandler
            .ExecuteAsync(request.AsCommand(), cancellationToken);

        return CreatedAtAction(nameof(GetOrderById), new { OrderNumber = request.OrderNumber }, null);
    }
}
