namespace Hotovec.Orders.Infrastructure.Persistence.MongoDb;

internal sealed class MongoOptions
{
    public string? ConnectionString { get; set; }
    
    public string? DatabaseName { get; set; }
    
    public string? CollectionName { get; set; }
    
    public string? UserName { get; set; }
    
    public string? Password { get; set; }
}
