using System.Text;
using System.Text.Json;
using Hotovec.Orders.Api;
using Hotovec.Orders.Api.Controllers.Orders.CreateOrder;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Hotovec.Orders.Integration.Test.UseCases;

/// <summary>
/// Takes configuration from API project and runs its instance. It needs already running
/// docker containers with database etc. Uses standard unmodified <see cref="WebApplicationFactory{TEntryPoint}" />
/// Needs internalsVisibleTo
/// Cannot be used in pipeline as containers will be missing
/// </summary>
public sealed class OrderTests(CustomWebApplicationFactory factory)
    : IClassFixture<CustomWebApplicationFactory>, IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    [Fact]
    public async Task CreateAndGetOrderAsync()
    {
        // Arrange
        const string orderNumber = "ORDER_1";
        var createOrderRequest = new CreateOrderRequest(
            orderNumber,
            "Joe Doe",
            "CZK",
            [
                new CreateOrderItem(1, "Item 1", 99.9m, 1),
                new CreateOrderItem(2, "Item 2", 199.9m, 2)
            ]);
        var createOrderJson = JsonSerializer.Serialize(createOrderRequest);
        var stringContent = new StringContent(createOrderJson, Encoding.UTF8, "application/json");
        var client = _factory.CreateClient();
        const string expected = "\"ORDER_1\"";

        // Act
        var actual = async () =>
        {
            var createResponse = await client.PostAsync("/api/Orders/Create", stringContent);
            createResponse.EnsureSuccessStatusCode();

            var queryResponse = await client.GetAsync($"/api/Orders/{orderNumber}");
            queryResponse.EnsureSuccessStatusCode();
            
            var responseContent = await queryResponse.Content.ReadAsStringAsync();
            
            // Assert
            responseContent.Should().Contain(expected);
            
            return responseContent;
        };

        // Assert
        await actual.Should().NotThrowAsync();
    }
    
    private bool _isDisposed;

    public ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            return ValueTask.CompletedTask;
        }
        
        var task = _factory.DisposeAsync();
        GC.SuppressFinalize(this);
        
        _isDisposed = true;
        
        return task;
    }
}
