using Hotovec.Orders.Domain.Common.Snapshots;
using Hotovec.Orders.Domain.Orders.Rounding;
using Hotovec.Orders.Domain.Orders.Snapshots;

namespace Hotovec.Orders.Domain.Orders.MonetaryInformation;

/// <summary>
/// Represents a monetary value consisting of an amount and a currency.
/// Value is rounded to two decimal places.
/// Only instances with the same currency can be compared.
/// </summary>
/// <remarks>
/// This class provides support for monetary calculations, comparisons,
/// equality checks, and formatting.
/// </remarks>
public sealed class Money : IEquatable<Money>, IComparable<Money>, IComparable, ISnapshotable<MoneySnapshot>
{
    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money FromSnapshot(MoneySnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        
        return new Money(snapshot.Amount!.Value, new Currency(snapshot.Currency!));
    }

    public decimal Amount { get; }

    public Currency Currency { get; }

    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        if (ReferenceEquals(this, obj))
        {
            return 0;
        }

        return obj is Money other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(Money)}");
    }

    public int CompareTo(Money? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        if (Currency != other.Currency)
        {
            var str = $"Unable to compare money with different currencies. Currencies: {Currency} and {other.Currency}";
            throw new ArgumentException(str, nameof(other));
        }

        return Amount.CompareTo(other.Amount);
    }

    public bool Equals(Money? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Amount == other.Amount && Currency.Equals(other.Currency);
    }

    public Money Round() => new(DecimalRoundingStrategy.Round(Amount), Currency);

    public MoneySnapshot ToSnapshot() => new(Amount, Currency.Code);

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is Money other && Equals(other));
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public static bool operator ==(Money? left, Money? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Money? left, Money? right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return $"{Currency} {Amount}";
    }

    public static bool operator <(Money left, Money right)
    {
        return Comparer<Money>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(Money left, Money right)
    {
        return Comparer<Money>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(Money left, Money right)
    {
        return Comparer<Money>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(Money left, Money right)
    {
        return Comparer<Money>.Default.Compare(left, right) >= 0;
    }

    private static void ThrowIfCurrenciesDoNotMatch(Money left, Money right)
    {
        if (left.Currency.Equals(right.Currency))
        {
            return;
        }

        var str =
            $"Unable to perform operations on money with different currencies. Currencies: {left.Currency} and {right.Currency}";
        
        throw new ArgumentException(str, nameof(right));
    }

    public static Money operator +(Money left, Money right)
    {
        ThrowIfCurrenciesDoNotMatch(left, right);
        
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        ThrowIfCurrenciesDoNotMatch(left, right);
        
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money left, Money right)
    {
        ThrowIfCurrenciesDoNotMatch(left, right);
        
        return new Money(left.Amount * right.Amount, left.Currency);
    }

    public static Money operator / (Money left, Money right)
    {
        ThrowIfCurrenciesDoNotMatch(left, right);
        
        return new Money(left.Amount / right.Amount, left.Currency);
    }
    
    public static Money operator +(Money left, decimal right)
    {
        return new Money(left.Amount + right, left.Currency);
    }
    
    public static Money operator +(decimal left, Money right)
    {
        return new Money(left + right.Amount, right.Currency);
    }
    
    public static Money operator -(Money left, decimal right)
    {
        return new Money(left.Amount - right, left.Currency);
    }
    
    public static Money operator -(decimal left, Money right)
    {
        return new Money(left - right.Amount, right.Currency);
    }

    public static Money operator *(Money left, decimal right)
    {
        return new Money(left.Amount * right, left.Currency);
    }
    
    public static Money operator *(decimal left, Money right)
    {
        return new Money(left * right.Amount, right.Currency);
    }

    public static Money operator / (Money left, decimal right)
    {
        return new Money(left.Amount / right, left.Currency);
    }
    
    public static Money operator / (decimal left, Money right)
    {
        return new Money(left / right.Amount, right.Currency);
    }
}
