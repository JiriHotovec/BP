using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hotovec.Orders.Infrastructure.Persistence.MongoDb;

internal sealed class MongoConnection : IDisposable, IMongoConnection
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoConnection(IOptions<MongoOptions> mongoOptions)
    {
        var options = mongoOptions.Value;
        _client = new MongoClient(options.ConnectionString);
        _database = _client.GetDatabase(options.DatabaseName);
    }
    
    public IMongoDatabase Database => _database;

    private bool _disposed;
    
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        _client.Dispose();
        _disposed = true;
    }
}
