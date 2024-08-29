using backlog_gamers_api.Config;
using backlog_gamers_api.Models.Enums;
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

    /// <summary>
    /// Constructor for initializing our private vars
    /// </summary>
    /// <param name="collection"></param>
    protected BaseRepository(string collection)
    {
        EnvironmentSettings envSettings = EnvironmentSettings.GetCurrentEnvSettings();
        _mongoClient = new MongoClient(envSettings.DbConnStr);
        _db = _mongoClient.GetDatabase(envSettings.DbName);
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

    /// <summary>
    /// Base get class that get a document by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity> Get(string id)
    {
        var builder = Builders<TEntity>.Filter;
        FilterDefinition<TEntity> filter = builder.Eq("_id", id);
        var res = await _collection.FindAsync(filter);
        return await res.FirstAsync();
    }

    /// <summary>
    /// Get all documents without any conditions
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<TEntity>> GetAll()
    {
        var res = await _collection.FindAsync(FilterDefinition<TEntity>.Empty);
        return res.ToList();
    }

    public Task<TEntity> GetByProperty(Type type, string propName, object val)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Post a single document to the database
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TEntity> Post(TEntity doc)
    {
        await _collection.InsertOneAsync(doc);
        return doc;
    }

    /// <summary>
    /// Post a list of documents to the database
    /// </summary>
    /// <param name="docs"></param>
    /// <returns></returns>
    public async Task<int> PostMultiple(List<TEntity> docs)
    {
        int count = docs.Count;
            
        if (count <= 0)
        {
            return 0;
        }
        //ignores skips duplicates
        var insertManyOptions = new InsertManyOptions { IsOrdered = false };
        
        await _collection.InsertManyAsync(docs, insertManyOptions);
        
        return count;
    }
}