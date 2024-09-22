using backlog_gamers_api.Models;
using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace backlog_gamers_api.Controllers;

/// <summary>
/// Controller for handling communication with articles
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController : ControllerBase
{
    public ArticleController(IArticlesRepository articlesRepository, IArticleSourceRepo sourceRepo)
    {
        _client = new HttpClient();
        _articlesRepository = articlesRepository;
        _gamingArticlesService = new GamingArticlesService();
        _articlesTagService = new ArticlesTagService(_articlesRepository);
        _sourceRepo = sourceRepo;
    }

    private readonly IArticlesRepository _articlesRepository;
    private readonly GamingArticlesService _gamingArticlesService;
    private readonly ArticlesTagService _articlesTagService;
    private readonly IArticleSourceRepo _sourceRepo;
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

    
    /// <summary>
    /// Add articles from external sources to our database
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ActionName("addExternalArticles")]
    public async Task<IActionResult> AddExternalArticles()
    {
        int totalArticles = 0;
        try
        {
            var sources = await _sourceRepo.GetAll();

            var articleSources = sources.ToList();
            if (articleSources.Count <= 0)
            {
                return NotFound("No Article sources have been set");
            }
            var articles = await _gamingArticlesService.GetExternalArticles(articleSources);

            // foreach (var article in articles)
            // {
            //     if (article == null || article.Content == null)
            //     {
            //         continue;
            //     } 
            //     var htmlDoc = new HtmlDocument();
            //     htmlDoc.LoadHtml(article.Content);
            //     string parsedText = htmlDoc.DocumentNode.InnerText;
            //     List<string> keywords = await _articlesTagService.GetKeywordsFromArticle(parsedText);
            //     List<ArticleTag> tags = _articlesTagService.CreateTagsFromKeywords(keywords);
            //
            //     article.Tags = tags.Select(tag => new MongoIdObject(tag.Id)).ToList();
            // }

            totalArticles = await _articlesRepository.CreateArticles(articles);
            return Ok(totalArticles);
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

    [HttpDelete]
    public async Task<ActionResult> DeleteDuplicates()
    {
        try
        {
            await _articlesRepository.FindDuplicates();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}