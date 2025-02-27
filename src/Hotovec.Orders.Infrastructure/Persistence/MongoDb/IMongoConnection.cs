using MongoDB.Driver;

namespace Hotovec.Orders.Infrastructure.Persistence.MongoDb;

internal interface IMongoConnection
{
    IMongoDatabase Database { get; }
}
