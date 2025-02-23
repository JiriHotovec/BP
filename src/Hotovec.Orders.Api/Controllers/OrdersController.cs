using Microsoft.AspNetCore.Mvc;

namespace Hotovec.Orders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController(ILogger<OrdersController> logger) : ControllerBase
{
    private readonly ILogger<OrdersController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<string> GetAll()
    {
        _logger.LogInformation("Getting all orders");
            
        return Enumerable.Range(1, 5)
            .Select(index => $"Order {index}")
            .ToArray();
    }
}