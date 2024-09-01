using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Repositories.Templates;

namespace backlog_gamers_api.Repositories;

public class ArticleTagRepo : BaseRepository<ArticleTag>, IArticleTagsRepo
{
    public ArticleTagRepo(string collection) : base(collection)
    {
    }
}