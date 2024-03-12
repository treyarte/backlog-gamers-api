using Azure;
using Azure.AI.TextAnalytics;
using backlog_gamers_api.Config;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Services.Interfaces;

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
        AzureKeyCredential credentials = new AzureKeyCredential(envSettings.AzureKeyCredential);
        Uri endpoint = new Uri(envSettings.AzureLangEndpoint);
        _client = new TextAnalyticsClient(endpoint, credentials);
        
        _articlesRepository = articlesRepository;
    }
    
    private readonly IArticlesRepository _articlesRepository;
    private readonly TextAnalyticsClient _client;

    /// <summary>
    /// Given an article will extract keywords from it using azure's key phrases extractor
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<List<string>> GetKeywordsFromArticle(string text)
    {
        try
        {
            var response = await _client.ExtractKeyPhrasesAsync(text);
            List<string> keywords = new List<string>();
            foreach (string keyPhrase in response.Value)
            {
                keywords.Add(keyPhrase);
            }

            return keywords;
        }
        catch (Exception e)
        {

            return new List<string>();
        }
    }
}