using System.Xml.Serialization;
using backlog_gamers_api.Models.Articles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using xmlParseExample.Models;
using xmlParseExample.Models.Enums;

namespace backlog_gamers_api.Services.Internal;

/// <summary>
/// Service for handling gaming news articles
/// TODO create interface
/// </summary>
public class GamingArticlesService
{
    private readonly HttpClient _client;
    
    public GamingArticlesService()
    {
        _client = new HttpClient();
    }
    
    /// <summary>
    /// Fetches 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private async Task<string> DownloadXML(string url)
    {
        HttpResponseMessage resp = await _client.GetAsync(url);
        resp.EnsureSuccessStatusCode();

        string xml = await resp.Content.ReadAsStringAsync();

        return xml;
    }
    
    /// <summary>
    /// Get a list of external articles from XML sources
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<List<Article>> GetArticlesFromXML(string url)
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
                        "",
                        item.MediaContent?.Url ?? "",
                        "");
                    
                    articles.Add(article);
                }
            }
        }

        return articles;
    }
    
    /// <summary>
    /// Get a list of external articles from JSON sources
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<List<Article>> GetArticlesFromJSON(string url)
    {
        List<ArticleWpJson> articlesFromWp = new List<ArticleWpJson>();
        List<Article> articles = new List<Article>();
        
        var res = await _client.GetAsync(url);

        if (!res.IsSuccessStatusCode)
        {
            return articles; 
        }
         
        string articleStr = await res.Content.ReadAsStringAsync();
         
        List<ArticleWpJson>? convertedArticles = JsonConvert.DeserializeObject<List<ArticleWpJson>>(articleStr);

        if (convertedArticles == null)
        {
            return articles;
        }
        
        foreach (ArticleWpJson article in convertedArticles)
        {
             articlesFromWp.Add(article);
        }

        foreach (var wpArticle in articlesFromWp)
        {
            Article article = new(
                wpArticle.TitleObj.Title,
                wpArticle.Link,
                "",
                wpArticle.ImgSrc,
                ""
            );
            if (string.IsNullOrWhiteSpace(wpArticle.ImgSrc))
            {
                //TODO check for null
                article.ImageUrl = wpArticle.Yoast.ImgSrcTwo.First().ImgUrl;
            }
            
            articles.Add(article);
        }

        return articles;
    }

    /// <summary>
    /// Get a list of articles from external websites
    /// </summary>
    /// <returns></returns>
    public async Task<List<Article>> GetExternalArticles()
    {
        List<Article> articlesList = new List<Article>();
        try
        {
            var getArticlesTasks = ArticleSourceList.Sources.Select(async article =>
            {
                switch (article.Type)
                {
                    case ArticleSourceType.Xml:
                        List<Article> articlesFromXml = await GetArticlesFromXML(article.RssUrl);
                        articlesList.AddRange(articlesFromXml);
                        break;
                    case ArticleSourceType.WordPressJson:
                        List<Article> articlesFromJson = await GetArticlesFromJSON(article.RssUrl);
                        articlesList.AddRange(articlesFromJson);
                        break;
                    default:
                        break;
                }
            });

            await Task.WhenAll(getArticlesTasks);

            return articlesList;
        }
        catch (Exception e)
        {
            //TODO log the error
            return new List<Article>();
        }
    }
}