using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using backlog_gamers_api.Services.Internal;
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
    }

    private readonly IArticlesRepository _articlesRepository;
    private readonly GamingArticlesService _gamingArticlesService;
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
        try
        {
            var articles = await _gamingArticlesService.GetExternalArticles();
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