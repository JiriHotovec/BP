using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hotovec.Orders.Infrastructure.Persistence.MongoDb;

internal sealed class MongoConnection : IDisposable, IMongoConnection
{
    private readonly TimeSpan _connectionTimeout = TimeSpan.FromSeconds(10);
    private readonly TimeSpan _serverSelectionTimeout = TimeSpan.FromSeconds(3);
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoConnection(IOptions<MongoOptions> mongoOptions)
    {
        var options = mongoOptions.Value;
        var mongoSettings = MongoClientSettings.FromConnectionString(options.ConnectionString);
        mongoSettings.ConnectTimeout = _connectionTimeout;
        mongoSettings.ServerSelectionTimeout = _serverSelectionTimeout;
        _client = new MongoClient(mongoSettings);
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
