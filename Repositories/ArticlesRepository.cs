using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Repositories.Templates;

namespace backlog_gamers_api.Repositories;

/// <summary>
/// Communicates with the articles collections 
/// </summary>
public class ArticlesRepository : BaseRepository<Article>, IArticlesRepository
{
    public ArticlesRepository(string conn, string db, string collection) : base(conn, db, collection)
    {
    }

    /// <summary>
    /// Create multiple articles
    /// </summary>
    /// <param name="articles"></param>
    /// <returns></returns>
    public async Task<int> Post(List<Article> articles)
    {
        try
        {
            int count = articles.Count;
            
            if (count <= 0)
            {
                return 0;
            }
            
            await _collection.InsertManyAsync(articles);
            return count;
        }
        catch (Exception e)
        {
            //TODO LOG Exception
            return 0;
        }
    }
}