namespace Hotovec.Orders.Domain.Common.Exceptions;

public sealed class DomainException(string message) : Exception(message);
