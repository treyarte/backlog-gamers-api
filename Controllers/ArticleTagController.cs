using backlog_gamers_api.Extensions;
using backlog_gamers_api.Helpers;
using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backlog_gamers_api.Controllers;

/// <summary>
/// Handles communication between the API and the
/// Article Tags Collection in the database
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleTagController : ControllerBase
{
    public ArticleTagController(IArticleTagsRepo articleTagsRepo)
    {
        _articleTagsRepo = articleTagsRepo;
    }

    private readonly IArticleTagsRepo _articleTagsRepo;

    [HttpGet]
    public async Task<ActionResult<List<ArticleTag>>> Get()
    {
        try
        {
            var res = await _articleTagsRepo.GetAll();
            return Ok(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<ArticleTag>> Get(string id)
    {
        try
        {
            ArticleTag? tag = await _articleTagsRepo.Get(id);

            if (tag == null)
            {
                return NotFound("Could not find tag");
            }
            return Ok(tag);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    /// <summary>
    /// Creates a new Article Tag
    /// </summary>
    /// <param name="articleTag"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ArticleTag>> Post(ArticleTag articleTag)
    {
        try
        {
            string slug = articleTag.Slug;
            slug = StringHelper.SanitizeSlug(slug);
            
            if (string.IsNullOrWhiteSpace(slug))
            {
                slug = articleTag.Name.ToSlug();
            }

            articleTag.Slug = slug;
            ArticleTag newTag = await _articleTagsRepo.Post(articleTag);

            return Ok(newTag);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(string id)
    {
        try
        {
            bool isDeleted = await _articleTagsRepo.Delete(id);
            return Ok(isDeleted);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}