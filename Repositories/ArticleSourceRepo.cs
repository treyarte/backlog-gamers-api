using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Repositories.Templates;
using xmlParseExample.Models;

namespace backlog_gamers_api.Repositories;

public class ArticleSourceRepo : BaseRepository<ArticleSource>, IArticleSourceRepo
{
    public ArticleSourceRepo(string collection) : base(collection)
    {
    }
}