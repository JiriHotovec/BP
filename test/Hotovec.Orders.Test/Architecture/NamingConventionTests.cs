using System.Reflection;
using Hotovec.Orders.Domain.Common.Entities;

namespace Hotovec.Orders.Test.Architecture;

public class NamingConventionTests
{
    [Fact]
    public void Interfaces_WithIPrefix_ReturnsTrue()
    {
        // Arrange
        string[] assemblyNames = [
            AssemblyConstants.ApiAssemblyName,
            AssemblyConstants.ApplicationAssemblyName,
            AssemblyConstants.DomainAssemblyName,
            AssemblyConstants.InfrastructureAssemblyName,
        ];
        var assemblies = assemblyNames.Select(Assembly.Load);
        
        // Act
        var actual = Types
            .InAssemblies(assemblies)
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        // Assert
        actual.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Entities_WithEntityPostfix_ReturnsTrue()
    {
        // Arrange
        string[] assemblyNames = [
            AssemblyConstants.DomainAssemblyName
        ];
        var assemblies = assemblyNames.Select(Assembly.Load);
        
        // Act
        var actual = Types
            .InAssemblies(assemblies)
            .That()
            .Inherit(typeof(Entity<,>))
            .Should()
            .HaveNameEndingWith("Entity")
            .GetResult();

        // Assert
        actual.IsSuccessful.Should().BeTrue();
    }
}
