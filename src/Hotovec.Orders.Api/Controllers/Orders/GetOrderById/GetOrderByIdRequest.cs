using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Hotovec.Orders.Api.Controllers.Orders.GetOrderById;

public sealed record GetOrderByIdRequest([Required] string OrderNumber);
