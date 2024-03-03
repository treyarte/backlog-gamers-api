using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Repositories.Templates;
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