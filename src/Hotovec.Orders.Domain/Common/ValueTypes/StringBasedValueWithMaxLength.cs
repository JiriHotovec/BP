namespace Hotovec.Orders.Domain.Common.ValueTypes;

public abstract class StringBasedValueWithMaxLength<T> : StringBasedValueType<T> where T : StringBasedValueType<T>
{
    protected StringBasedValueWithMaxLength(
        string value,
        int maxLength,
        StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        : base(value, comparisonType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"Value cannot be longer than {maxLength} characters", nameof(value));
        }
    }
}
