using System.Text.RegularExpressions;
using Hotovec.Orders.Domain.Common.Snapshots;
using Hotovec.Orders.Domain.Common.ValueTypes;

namespace Hotovec.Orders.Domain.Orders;

public sealed partial class OrderNumber : StringBasedValueWithMaxLength<OrderNumber>, ISnapshotable<string>
{
    private static readonly Regex ParsingRegex = ParserRegex();

    public int Number { get; }

    public OrderNumber(string value) : base(value,150)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        var normalized = value.ToUpperInvariant();
        var match = ParsingRegex.Match(normalized);
        if (!match.Success)
        {
            throw new ArgumentException("Provided value is not a valid order number. A string in format 'ORDER_NUMBER' is expected", nameof(value));
        }
        
        Number = int.Parse(match.Groups["number"].Value);
    }

    [GeneratedRegex(@"^ORDER_(?<number>\d+)$",
        RegexOptions.IgnoreCase |
        RegexOptions.Singleline |
        RegexOptions.CultureInvariant)]
    private static partial Regex ParserRegex();

    public string ToSnapshot()
    {
        return Value;
    }
}

