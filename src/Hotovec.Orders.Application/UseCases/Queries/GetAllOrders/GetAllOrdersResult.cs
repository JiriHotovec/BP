﻿using Hotovec.Orders.Domain.Orders;

namespace Hotovec.Orders.Application.UseCases.Queries.GetAllOrders;

public sealed record GetAllOrdersResult(List<OrderEntity> Orders);
