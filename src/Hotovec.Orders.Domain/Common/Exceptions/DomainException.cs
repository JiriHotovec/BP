namespace Hotovec.Orders.Domain.Common.Exceptions;

/// <summary>
/// Represents an exception specifically related to domain errors.
/// This exception is used to indicate improper behavior or invalid operations
/// that break the business rules or constraints of the domain model.
/// </summary>
/// <remarks>
/// This exception inherits from the base <see cref="Exception"/> class and provides
/// constructors to create domain-specific error messages or include inner exceptions
/// for more detailed error context and troubleshooting.
/// </remarks>
/// <example>
/// Use this exception to signify violations of domain invariants or rules specific to the application.
/// For instance:
/// - Adding a duplicate item to an order.
/// - Transitions within a domain entity to an invalid state.
/// </example>
public sealed class DomainException : Exception
{
    /// <summary>
    /// Represents an exception that occurs within the domain logic of the application.
    /// </summary>
    public DomainException()
    {
    }

    /// <summary>
    /// Represents an exception that occurs within the domain layer of the application.
    /// </summary>
    public DomainException(string message) : base(message)
    {
    }

    /// <summary>
    /// Represents errors that occur within the domain layer of the application.
    /// </summary>
    public DomainException(string message, Exception inner) : base(message, inner)
    {
    }
}
