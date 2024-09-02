using System;
using System.Threading.Tasks;
using backlog_gamers_api.Repositories;
using backlog_gamers_api.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ArticleScheduler;

public static class GetArticlesDaily
{
    private static GamingArticlesService _gamingService = new GamingArticlesService();
    private static ArticlesRepository _articlesRepo = new ArticlesRepository("articles");
    
    [FunctionName("GetArticlesDaily")]
    public static async Task RunAsync([TimerTrigger("0 */10 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

        int articlesCreated = await GetAndInsertArticles(log);

        log.LogInformation("Articles created: ${articlesCreated}");
    }
    
    private static async Task<int> GetAndInsertArticles(ILogger log)
    {
        try
        {
            var articles = await _gamingService.GetExternalArticles();
        
            int addCount = await _articlesRepo.PostMultiple(articles);

            return addCount;
        }
        catch (Exception e)
        {
            log.LogError(e.Message);
            throw;
        }

    }
}