using Hotovec.Orders.Domain.Common.Entities;
using Hotovec.Orders.Domain.Common.Exceptions;
using Hotovec.Orders.Domain.Orders.Dtos;
using Hotovec.Orders.Domain.Orders.Snapshots;
using Hotovec.Orders.Domain.Orders.MonetaryInformation;

namespace Hotovec.Orders.Domain.Orders;

public sealed class OrderEntity : Entity<OrderNumber,OrderSnapshot>
{
    private readonly List<OrderItemEntity> _orderItems = [];
    
    public OrderEntity(
        OrderNumber orderNumber,
        string customerName,
        Currency currency,
        DateTimeOffset dateCreated,
        params OrderItemDto[] items)
    : base(orderNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(customerName);
        ArgumentNullException.ThrowIfNull(items);
        
        CustomerName = customerName;
        Currency = currency;
        DateCreated = dateCreated;
        var localItems = items.ToArray();
        
        ValidateAtLeastOneItemPresent(localItems);
        ValidateAllItemsHaveSameCurrencyAsOrder(localItems, currency);
        
        for (int i = 0; i < localItems.Length; i++)
        {
            var currentItem = localItems[i];
            var orderItem = new OrderItemEntity(i+1, currentItem.ProductName, currentItem.UnitPrice, currentItem.Quantity);
            _orderItems.Add(orderItem);    
        }
    }
    
    public OrderEntity(OrderSnapshot snapshot)
        : base(new OrderNumber(snapshot?.Id ?? string.Empty))
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        
        Currency = new Currency(snapshot.Currency!);
        CustomerName = snapshot.CustomerName!;
        DateCreated = DateTimeOffset.FromUnixTimeMilliseconds(snapshot.DateCreated!.Value);
        _orderItems.AddRange(snapshot.Items.Select(i => new OrderItemEntity(i)));
    }
    
    public DateTimeOffset DateCreated { get; }

    public Currency Currency { get; }

    public string CustomerName { get; private set; }
    
    public IEnumerable<OrderItemDto> GetAllItems()
    {
        return _orderItems.Select(x => new OrderItemDto()
        {
            ProductName = x.ProductName,
            UnitPrice = x.UnitPrice,
            Quantity = x.Quantity
        });
    }

    public void ChangeCustomer(string customerName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(customerName);
        
        CustomerName = customerName;
    }
    
    public override OrderSnapshot ToSnapshot() =>
        new(Id.ToString(),
            CustomerName,
            DateCreated.ToUnixTimeMilliseconds(),
            Currency.Code,
            _orderItems.Select(x => x.ToSnapshot()).ToArray());

    private static void ValidateAtLeastOneItemPresent(OrderItemDto[] items)
    {
        ArgumentNullException.ThrowIfNull(items);
        
        if (items.Length == 0)
        {
            throw new DomainException("Unable to create order. At least one item must be provided");
        }
    }

    private static void ValidateAllItemsHaveSameCurrencyAsOrder(OrderItemDto[] items, Currency orderCurrency)
    {
        ArgumentNullException.ThrowIfNull(items);
        
        if (items.Any(i => i.UnitPrice.Currency != orderCurrency))
        {
            throw new DomainException($"Unable to create order. All items must have the same currency ({orderCurrency}) as the order.");    
        }
    }
}
