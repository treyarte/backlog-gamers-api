using backlog_gamers_api.Models;
using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Services;
using HtmlAgilityPack;
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
    public ArticleController(IArticlesRepository articlesRepository)
    {
        _client = new HttpClient();
        _articlesRepository = articlesRepository;
        _gamingArticlesService = new GamingArticlesService();
        _articlesTagService = new ArticlesTagService(_articlesRepository);
    }

    private readonly IArticlesRepository _articlesRepository;
    private readonly GamingArticlesService _gamingArticlesService;
    private readonly ArticlesTagService _articlesTagService;
    private readonly HttpClient _client;
    
    /// <summary>
    /// Add articles from external sources to our database
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("getAllArticles")]
    public async Task<IActionResult> GetArticles()
    {
        try
        {
            IEnumerable<Article> articles = await _articlesRepository.GetAll();
            var enumerable = articles.ToList();
            List<Article> first10Articles = enumerable.ToList().TakeLast(10).ToList();
            return Ok(first10Articles);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve articles");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetArticleKeywords([FromQuery] string text)
    {
        try
        {
            var list = await _articlesTagService.GetKeywordsFromArticle(text);
            return Ok(list);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    /// <summary>
    /// Add articles from external sources to our database
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ActionName("addExternalArticles")]
    public async Task<IActionResult> AddExternalArticles()
    {
        try
        {
            var articles = await _gamingArticlesService.GetExternalArticles();

            foreach (var article in articles)
            {
                if (article == null || article.Content == null)
                {
                    continue;
                } 
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(article.Content);
                string parsedText = htmlDoc.DocumentNode.InnerText;
                List<string> keywords = await _articlesTagService.GetKeywordsFromArticle(parsedText);
                List<ArticleTag> tags = _articlesTagService.CreateTagsFromKeywords(keywords);

                article.Tags = tags.Select(tag => new MongoIdObject(tag.Id)).ToList();
            }
            
            int addCount = await _articlesRepository.PostMultiple(articles);
            return Ok(addCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add external articles");
        }
    }

    /// <summary>
    /// Removes articles that are no longer relevant 
    /// </summary>
    /// <returns></returns>
    // [HttpGet]
    // [ActionName("DeleteOldArticles")] //TODO archive instead of delete and protect this route
    // public async Task<ActionResult<int>> DeleteOldArticles()
    // {
    //
    //     try
    //     {
    //
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Failed to remove old articles");
    //     }
    // }
    //
    /// <summary>
    /// For Testing only, deletes all articles in the db
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("DeleteAll")] //TODO remove this when launch
    public async Task<ActionResult<int>> DeleteAll()
    {

        try
        {
            int deleteCount = _articlesRepository.DeleteAll();
            return Ok(deleteCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete articles");
        }
    }
    
}