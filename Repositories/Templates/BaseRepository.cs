using backlog_gamers_api.Repositories.Templates.Interfaces;
using MongoDB.Driver;

namespace backlog_gamers_api.Repositories.Templates;

/// <summary>
/// Base repo class that setups a mongoclient for communication with the db and collections
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class BaseRepository<TEntity>:IBaseRepository<TEntity> where TEntity : class
{
    private MongoClient _mongoClient;
    private IMongoDatabase _db;
    protected readonly IMongoCollection<TEntity> _collection;

    protected BaseRepository(string conn, string db, string collection)
    {
        _mongoClient = new MongoClient(conn);
        _db =  _mongoClient.GetDatabase(db);
        _collection = _db.GetCollection<TEntity>(collection);
    }

    /// <summary>
    /// Returns the current collection
    /// </summary>
    /// <returns></returns>
    public IMongoCollection<TEntity> GetCollection()
    {
        return _collection;
    }
}