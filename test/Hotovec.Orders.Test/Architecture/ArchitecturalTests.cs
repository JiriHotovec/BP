using System.Reflection;
using Hotovec.Orders.Domain.Common.Entities;

namespace Hotovec.Orders.Test.Architecture;

public class ArchitecturalTests
{
    [Fact]
    public void Interfaces_WithIPrefix_ReturnsTrue()
    {
        // Arrange
        string[] assemblyNames = [
            "Hotovec.Orders.Api",
            "Hotovec.Orders.Application",
            "Hotovec.Orders.Domain",
            "Hotovec.Orders.Infrastructure",
        ];
        var assemblies = assemblyNames.Select(Assembly.Load);
        
        // Act
        var result = Types
            .InAssemblies(assemblies)
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public void Entities_WithEntityPostfix_ReturnsTrue()
    {
        // Arrange
        string[] assemblyNames = [
            "Hotovec.Orders.Domain"
        ];
        var assemblies = assemblyNames.Select(Assembly.Load);
        
        // Act
        var result = Types
            .InAssemblies(assemblies)
            .That()
            .Inherit(typeof(Entity<,>))
            .Should()
            .HaveNameEndingWith("Entity")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }
}
