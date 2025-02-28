using System.Text;
using System.Text.Json;
using Hotovec.Orders.Api;
using Hotovec.Orders.Api.Controllers.Orders.CreateOrder;
using Hotovec.Orders.Api.Controllers.Orders.GetAllOrders;
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
    public async Task CreateAndGetAllOrdersAsync()
    {
        // Arrange
        const string orderNumber = "ORDER_100";
        var createOrderRequest = new CreateOrderRequest(
            orderNumber,
            "Josh Bosh",
            "USD",
            [
                new CreateOrderItem(1, "Item 1", 99.9m, 1)
            ]);
        var createOrderJson = JsonSerializer.Serialize(createOrderRequest);
        var stringContent = new StringContent(createOrderJson, Encoding.UTF8, "application/json");
        const string orderNumber2 = "ORDER_101";
        var createOrderRequest2 = new CreateOrderRequest(
            orderNumber2,
            "Jack Black",
            "EUR",
            [
                new CreateOrderItem(11, "Item 11", 199.9m, 12)
            ]);
        var createOrderJson2 = JsonSerializer.Serialize(createOrderRequest2);
        var stringContent2 = new StringContent(createOrderJson2, Encoding.UTF8, "application/json");
        var client = _factory.CreateClient();
        const int expected = 2;

        // Act
        var actual = async () =>
        {
            var createResponse = await client.PostAsync("/api/Orders/Create", stringContent);
            createResponse.EnsureSuccessStatusCode();
            
            var createResponse2 = await client.PostAsync("/api/Orders/Create", stringContent2);
            createResponse2.EnsureSuccessStatusCode();

            var queryResponse = await client.GetAsync($"/api/Orders");
            queryResponse.EnsureSuccessStatusCode();
            
            var responseContent = await queryResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<GetAllOrdersResponse>(responseContent, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            
            // Assert
            response.Should().NotBeNull();
            response.OrderNumbers.Should().HaveCount(expected);
        };

        // Assert
        await actual.Should().NotThrowAsync();
    }
    
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
        };

        // Assert
        await actual.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task CreateAndDeleteOrderAsync()
    {
        // Arrange
        const string orderNumber = "ORDER_10";
        var createOrderRequest = new CreateOrderRequest(
            orderNumber,
            "Jack Black",
            "EUR",
            [
                new CreateOrderItem(1, "Item 1", 299.9m, 10)
            ]);
        var createOrderJson = JsonSerializer.Serialize(createOrderRequest);
        var stringContent = new StringContent(createOrderJson, Encoding.UTF8, "application/json");
        var client = _factory.CreateClient();

        // Act
        var actual = async () =>
        {
            var createResponse = await client.PostAsync("/api/Orders/Create", stringContent);
            createResponse.EnsureSuccessStatusCode();

            var queryResponse = await client.DeleteAsync($"/api/Orders/{orderNumber}");
            queryResponse.EnsureSuccessStatusCode();
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
