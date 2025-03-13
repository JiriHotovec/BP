namespace Hotovec.Orders.Architecture.Test;

[Category("architecture")]
public sealed class DependenciesTests
{
    [Fact]
    public void DomainLayer_CorrectDependencies_ReturnsTrue()
    {
        // Arrange
        // Act
        var result = Types
            .InAssembly(AssemblyConstants.DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(GetResultMessage(result));
    }
    
    [Fact]
    public void DomainLayer_SealedObjects_ReturnsTrue()
    {
        // Arrange
        // Act
        var result = Types
            .InAssembly(AssemblyConstants.DomainAssembly)
            .That()
            .AreClasses()
            .And()
            .AreNotAbstract()
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(GetResultMessage(result));
    }
    
    [Fact]
    public void ApplicationLayer_CorrectDependencies_ReturnsTrue()
    {
        // Arrange
        // Act
        var result = Types
            .InAssembly(AssemblyConstants.ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(
                AssemblyConstants.ApiAssemblyName,
                AssemblyConstants.InfrastructureAssemblyName)
            .And()
            .HaveDependencyOn(AssemblyConstants.DomainAssemblyName)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(GetResultMessage(result));
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
            .And()
            .HaveDependencyOnAll(
                AssemblyConstants.DomainAssemblyName,
                AssemblyConstants.ApplicationAssemblyName)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(GetResultMessage(result));
    }
    
    [Fact]
    public void ApiLayer_CorrectDependencies_ReturnsTrue()
    {
        // Arrange
        // Act
        var result = Types
            .InAssembly(AssemblyConstants.InfrastructureAssembly)
            .Should()
            .HaveDependencyOnAll()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(GetResultMessage(result));
    }
    
    private string GetResultMessage(TestResult result) =>
        result.FailingTypeNames != null
            ? string.Join(", ", result.FailingTypeNames)
            : string.Empty;
}
