using System.Reflection;

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
}
