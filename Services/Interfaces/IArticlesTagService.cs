namespace backlog_gamers_api.Services.Interfaces;

public interface IArticlesTagService
{
    public Task<List<string>> GetKeywordsFromArticle(string text);
}