using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using xmlParseExample.Models;
using xmlParseExample.Models.Enums;

namespace backlog_gamers_api.Controllers;

/// <summary>
/// Controller for handling communication with articles
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController : ControllerBase
{
    public ArticleController()
    {
        _client = new HttpClient();
    }

    private readonly HttpClient _client;

    /// <summary>
    /// Extract article data from articles sources to
    /// update the database with new articles
    /// </summary>
    /// <param name="url"></param>
    /// <returns>bool</returns>
    [HttpGet]
    [ActionName("refresh-articles")]
    public async Task<IActionResult> RefreshArticles([FromQuery] string url)
    {
        List<ArticleWpJson> articlesFromWp = new List<ArticleWpJson>();
        try
        {
            
            var getArticleTasks = ArticleSourceList.Sources.Select(async source =>
            {
                if (source.Type != ArticleSourceType.WordPressJson) { return; }
                
                var res = await _client.GetAsync(source.RssUrl);
                
                if (!res.IsSuccessStatusCode) { return; }

                string articleStr = await res.Content.ReadAsStringAsync();
                List<ArticleWpJson>? convertedArticles = JsonConvert.DeserializeObject<List<ArticleWpJson>>(articleStr);

                if (convertedArticles == null) { return; }

                foreach (ArticleWpJson article in convertedArticles)
                {
                    articlesFromWp.Add(article);
                }
            });

            await Task.WhenAll(getArticleTasks);

            return Ok(articlesFromWp);
        }
        catch (Exception e)
        {
            //TODO add logging 
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to refresh articles");
        }
    }
}