using backlog_gamers_api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using xmlParseExample.Models;

namespace backlog_gamers_api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleSourceController : ControllerBase
{
    public ArticleSourceController(IArticleSourceRepo repo)
    {
        _repo = repo;
    }

    private IArticleSourceRepo _repo;
    
    [HttpPost]
    public async Task<ActionResult<ArticleSource>> Post(ArticleSource source)
    {
        try
        {
            var res = await _repo.Post(source);
            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> PostMulti(List<ArticleSource> sources)
    {
        try
        {
            var res = await _repo.PostMultiple(sources);
            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}