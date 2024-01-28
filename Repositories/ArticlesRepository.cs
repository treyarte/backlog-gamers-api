using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Repositories.Templates;

namespace backlog_gamers_api.Repositories;

/// <summary>
/// Communicates with the articles collections 
/// </summary>
public class ArticlesRepository : BaseRepository<Article>, IArticlesRepository
{
    public ArticlesRepository(string collection) : base(collection)
    {
    }
}