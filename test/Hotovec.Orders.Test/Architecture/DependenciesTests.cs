namespace Hotovec.Orders.Test.Architecture;

[Category("architecture")]
public sealed class DependenciesTests
{
    [Fact]
    public void DomainLayer_CorrectDependencies_ReturnsTrue()
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
    public void ApplicationLayer_CorrectDependencies_ReturnsTrue()
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
    public void InfrastructureLayer_CorrectDependencies_ReturnsTrue()
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
