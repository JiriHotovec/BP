using System.Diagnostics.CodeAnalysis;

namespace Hotovec.Orders.Domain.Common.ValueTypes;

/// <summary>
/// Represents an abstract base class for creating value types based on a string.
/// This class provides equality comparison and string representation functionality
/// using a specified <see cref="StringComparison"/>.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Abstract class")]
public abstract class StringBasedValueType<T> : IEquatable<T> where T : StringBasedValueType<T>
{
    /// Gets the string representation of the value encapsulated by the string-based value type.
    /// This property represents the actual data held by the type.
    public string Value { get; }

    public StringComparison ComparisonType { get; }

    /// Represents a value type that uses a string as its underlying value with optional string comparison rules.
    /// This abstract class is intended to be the base class for types that encapsulate string-based values
    /// with specific comparison rules, providing equality checks, hash code calculation, and string representation.
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

    /// <summary>
    /// Calculates and returns the hash code for the current string-based value.
    /// </summary>
    /// <returns>
    /// An integer hash code that represents the hash of the current string value
    /// considering the specified string comparison type.
    /// </returns>
    public override int GetHashCode()
    {
        return string.GetHashCode(Value, ComparisonType);
    }

    /// <summary>
    /// Returns the string representation of the current object.
    /// </summary>
    /// <returns>
    /// A string that represents the current object.
    /// </returns>
    public override string ToString()
    {
        return Value;
    }
}
