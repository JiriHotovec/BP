using System.Diagnostics.CodeAnalysis;

namespace Hotovec.Orders.Domain.Common.ValueTypes;

[ExcludeFromCodeCoverage(Justification = "Abstract class")]
public abstract class StringBasedValueType<T> : IEquatable<T> where T : StringBasedValueType<T>
{
    public string Value { get; }

    public StringComparison ComparisonType { get; }

    protected StringBasedValueType(
        string value,
        StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        Value = value;
        ComparisonType = comparisonType;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is T other && Equals(other));
    }

    public bool Equals(T? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(other, this))
        {
            return true;
        }

        return string.Compare(Value, other.Value, ComparisonType ) == 0;
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(Value, ComparisonType);
    }

    public override string ToString()
    {
        return Value;
    }
}
