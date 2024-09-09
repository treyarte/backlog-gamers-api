using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Repositories.Templates;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backlog_gamers_api.Repositories;

/// <summary>
/// Communicates with the articles collections 
/// </summary>
public class ArticlesRepository : BaseRepository<Article>, IArticlesRepository
{
    public ArticlesRepository(string collection) : base(collection)
    {
    }
    
    /// <summary>
    /// Insert only new articles and skip duplicates
    /// </summary>
    /// <param name="articles"></param>
    /// <returns></returns>
    public async Task<int> CreateArticles(List<Article> articles)
    {
        try
        {
            //Allows us to ignore the duplicate error and continue inserting articles
            var insertManyOptions = new InsertManyOptions { IsOrdered = false };
            await _collection.InsertManyAsync(articles, insertManyOptions);
            return articles.Count;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }

    /// <summary>
    /// Find duplicates 
    /// </summary>
    /// <returns></returns>
    public async Task FindDuplicates()
    {
        PipelineDefinition<Article, BsonDocument> pipeLine = new[]
        {

            new BsonDocument("$group",
                new BsonDocument
                {
                    { "_id", "$url" },
                    {
                        "count",
                        new BsonDocument("$sum", 1)
                    }
                }),
            new BsonDocument("$match",
                new BsonDocument
                {
                    {
                        "_id",
                        new BsonDocument("$ne", BsonNull.Value)
                    },
                    {
                        "count",
                        new BsonDocument("$gt", 1)
                    }
                }),
            new BsonDocument("$project",
                new BsonDocument
                {
                    { "url", "$_id" },
                    { "_id", 0 }
                })

        };
        
        var duplicates = await _collection.Aggregate(pipeLine).ToListAsync();

        foreach (var group in duplicates)
        {
            var url = group["url"];
            var filter = Builders<Article>.Filter.Eq("url", url);
            await _collection.DeleteOneAsync(filter);
        }
    }

    /// <summary>
    /// Delete all articles in the db
    /// </summary>
    /// <returns></returns>
    public int DeleteAll()
    {
        var res = _collection.DeleteMany(FilterDefinition<Article>.Empty);

        if (res.IsAcknowledged)
        {
            int deleteCount = (int)res.DeletedCount;
            return deleteCount;
        }

        return 0;
    }
}