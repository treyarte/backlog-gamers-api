
using backlog_gamers_api.Config;
using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Services.Interfaces;
using MongoDB.Bson;

namespace backlog_gamers_api.Services;

/// <summary>
/// Service for handling the creation of tags that are match to articles
/// </summary>
public class ArticlesTagService : IArticlesTagService
{
    public ArticlesTagService(IArticlesRepository articlesRepository)
    {
        //Setup for Azure Analytics text client
        EnvironmentSettings envSettings = EnvironmentSettings.GetCurrentEnvSettings();
        
        _articlesRepository = articlesRepository;
    }
    
    private readonly IArticlesRepository _articlesRepository;

    
}