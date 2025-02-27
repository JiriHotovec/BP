namespace Hotovec.Orders.Test.Architecture;

public sealed class DependenciesTests
{
    [Fact]
    public void Domain_CorrectDependencies_ReturnsTrue()
    {
        // Arrange
        // Act
        var actual = Types
            .InAssembly(AssemblyConstants.DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(
                AssemblyConstants.ApiAssemblyName,
                AssemblyConstants.ApplicationAssemblyName,
                AssemblyConstants.InfrastructureAssemblyName)
            .GetResult();

        // Assert
        actual.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Application_CorrectDependencies_ReturnsTrue()
    {
        // Arrange
        // Act
        var actual = Types
            .InAssembly(AssemblyConstants.ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(
                AssemblyConstants.ApiAssemblyName,
                AssemblyConstants.InfrastructureAssemblyName)
            .GetResult();

        // Assert
        actual.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_CorrectDependencies_ReturnsTrue()
    {
        // Arrange
        // Act
        var result = Types
            .InAssembly(AssemblyConstants.InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(AssemblyConstants.ApiAssemblyName)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
