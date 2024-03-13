using backlog_gamers_api.Models.Articles;
using xmlParseExample.Models.Enums;

namespace backlog_gamers_api.Services.Interfaces;

public interface IGamingArticlesService
{
    public Task<List<Article>> GetArticlesFromXML(string url, ArticleSiteEnum articleSite);
    public Task<List<Article>> GetArticlesFromJson(string url, ArticleSiteEnum articleSite);
    public Task<List<Article>> GetExternalArticles();
}