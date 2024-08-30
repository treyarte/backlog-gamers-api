using System.Xml.Serialization;
using backlog_gamers_api.Helpers;
using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Services.Interfaces;
using Newtonsoft.Json;
using xmlParseExample.Models;
using xmlParseExample.Models.Enums;

namespace backlog_gamers_api.Services;

/// <summary>
/// Service for handling gaming news articles
/// </summary>
public class GamingArticlesService:IGamingArticlesService
{
    public GamingArticlesService()
    {
        _client = new HttpClient();
    }
    
    private readonly HttpClient _client;
    
    /// <summary>
    /// Fetches 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private async Task<string> DownloadXML(string url)
    {
        try
        {
            HttpResponseMessage resp = await _client.GetAsync(url);
            resp.EnsureSuccessStatusCode();

            string xml = await resp.Content.ReadAsStringAsync();

            return xml;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "";
        }
    }

    /// <summary>
    /// Get a list of external articles from XML sources
    /// </summary>
    /// <param name="url"></param>
    /// <param name="articleSite"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<List<Article>> GetArticlesFromXML(string url, ArticleSiteEnum articleSite)
    {
        string xml = await DownloadXML(url);
        List<Article> articles = new List<Article>();

        if (string.IsNullOrWhiteSpace(xml))
        {
            return articles;
        }

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
                    var itemDate = DateHelper.ConvertStrToDate(item.PubDate);

                    bool isToday = itemDate.Date == DateTimeOffset.Now.Date;

                    if (!isToday)
                    {
                        continue;
                    }
                    
                    Article article = new(
                        item.Title,
                        articleSite,
                        item.Link,
                        item.Description,
                        item.MediaContent?.Url ?? item.Media?.Url ?? "",
                        item.ContentEncoded,
                        DateHelper.ConvertStrToDate(item.PubDate),
                        new ArticleStats()
                        );
                    
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
    /// <param name="articleSit"></param>
    /// <returns></returns>
    public async Task<List<Article>> GetArticlesFromJson(string url, ArticleSiteEnum articleSite)
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
                articleSite,
                wpArticle.Link,
                "",
                wpArticle.ImgSrc,
                wpArticle.ContentObj.Content,
                DateHelper.ConvertStrToDate(wpArticle.DateString),
                new ArticleStats()
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
            var getArticlesTasks = ArticleSourceList.Sources.Select(async source =>
            {
                switch (source.Type)
                {
                    case ArticleSourceType.Xml:
                        List<Article> articlesFromXml = await GetArticlesFromXML(source.RssUrl, source.ArticleSite);
                        articlesList.AddRange(articlesFromXml);
                        break;
                    // case ArticleSourceType.WordPressJson:
                    //     List<Article> articlesFromJson = await GetArticlesFromJson(source.RssUrl, source.ArticleSite);
                    //     articlesList.AddRange(articlesFromJson);
                    //     break;
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