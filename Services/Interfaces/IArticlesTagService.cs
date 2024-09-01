using backlog_gamers_api.Models.Articles;

namespace backlog_gamers_api.Services.Interfaces;

public interface IArticlesTagService
{
    public Task<List<string>> GetKeywordsFromArticle(string text);
}