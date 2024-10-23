using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Templates.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backlog_gamers_api.Repositories.Interfaces;

public interface IArticleTagsRepo : IBaseRepository<ArticleTag>
{
}