using System.Text.RegularExpressions;
using Hotovec.Orders.Domain.Common.Snapshots;
using Hotovec.Orders.Domain.Common.ValueTypes;

namespace Hotovec.Orders.Domain.Orders;

/// <summary>
/// Represents an order number as a value type derived from string.
/// Ensures the correct format of the order number while allowing
/// further property extraction such as the numerical part of the order.
/// </summary>
public sealed partial class OrderNumber : StringBasedValueWithMaxLength<OrderNumber>, ISnapshotable<string>
{
    /// <summary>
    /// Contains a compiled regular expression used to parse and validate order number strings.
    /// This field is derived from the <see cref="ParserRegex"/> method, which ensures that
    /// all order number strings conform to a specific pattern.
    /// </summary>
    private static readonly Regex ParsingRegex = ParserRegex();

    /// <summary>
    /// Represents an order number as a value type with validation and parsing functionality.
    /// </summary>
    /// <remarks>
    /// The <see cref="OrderNumber"/> class ensures that the provided value conforms to a specific format
    /// ("ORDER_NUMBER") and extracts the numeric portion of the order number for further processing.
    /// </remarks>
    public int Number { get; }

    /// <summary>
    /// Represents a domain-specific value type for order numbers, encapsulating validation, normalization,
    /// and extraction of the numeric component from an order string.
    /// </summary>
    /// <remarks>
    /// This type ensures that all order numbers adhere to a predefined format through validation upon construction.
    /// The numeric component of the order number is parsed and exposed as a property for further use.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided value does not conform to the required order number format.
    /// </exception>
    /// <seealso cref="StringBasedValueType"/>
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

    /// <summary>
    /// Provides a compiled regular expression used for parsing and validation of order numbers.
    /// The pattern matches strings that conform to the "ORDER_number" format, where number denotes
    /// an integer representing the order number. The regex is configured to ensure case-insensitivity,
    /// culture-invariance, and singleline matching behavior for enhanced robustness and performance.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="Regex"/> class pre-configured with the matching pattern,
    /// enabling efficient extraction and validation of the numeric component from order number strings.
    /// </returns>
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

