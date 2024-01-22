using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using xmlParseExample.Models;

namespace xmlParseExample.Controllers;

[ApiController]
[Route("[controller]")]
public class XmlController : ControllerBase
{
    private readonly HttpClient _client;
    public XmlController()
    {
        _client = new HttpClient();
    }

    private async Task<string> DownloadXML(string url)
    {
        HttpResponseMessage resp = await _client.GetAsync(url);
        resp.EnsureSuccessStatusCode();

        string xml = await resp.Content.ReadAsStringAsync();

        return xml;
    }
    
    [HttpGet(Name = "ParseXML")]
    public async Task<IActionResult> Get([FromQuery] string url)
    {
        try
        {
            string xml = await DownloadXML(url);
            List<Article> articles = new List<Article>();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("atom", "http://www.w3.org/2005/Atom");
            namespaces.Add("content", "http://purl.org/rss/1.0/modules/content/");
            namespaces.Add("dc", "http://purl.org/dc/elements/1.1/");
            namespaces.Add("media", "http://search.yahoo.com/mrss/");
            
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RssFeed));
                {
                    var rssFeed = (RssFeed)serializer.Deserialize(stringReader);

                    //TODO check if exists
                    if (rssFeed == null || rssFeed.Channel == null)
                    {
                        throw new NullReferenceException("Rss feed or channel is null");
                    }
                    
                    foreach (var item in rssFeed.Channel.Items)
                    {
                        Article article = new(
                            item.Title,
                            item.Link,
                            item.Description,
                            item.MediaContent?.Url ?? "",
                            item.ContentEncoded ?? "");
                        
                        articles.Add(article);
                    }
                    return Ok(articles);
                }
            }

            return Ok(null);
        }
        catch (Exception e)
        {
            //Log error
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
}