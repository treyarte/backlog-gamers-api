using System.Xml.Serialization;
using backlog_gamers_api.Extensions;
using backlog_gamers_api.Helpers;
using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Models.Data;
using backlog_gamers_api.Services.Interfaces;
using Ganss.Xss;
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
        _sanitizer = new HtmlSanitizer();
        _sanitizer.AllowedAttributes.Add("");
        // _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36");
        // _client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
        // _client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.5");
        // _client.DefaultRequestHeaders.Referrer = new Uri("https://www.google.com/");
    }
    
    private readonly HttpClient _client;
    private readonly HtmlSanitizer _sanitizer;
    
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
                    
                    //TODO add check to see if date is within the past 2 weeks
                    // bool isToday = itemDate.Date == DateTimeOffset.Now.Date;
                    //
                    // if (!isToday)
                    // {
                    //     continue;
                    // }
                    
                    Article article = new(
                        item.Title,
                        articleSite,
                        item.Link,
                        item.Title.ToSlug(),
                        string.IsNullOrWhiteSpace(item.Description) ? 
                            HtmlHelper.StripHtml(item.ContentEncoded.Substring(0, 200)) 
                            : item.Description,
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
                wpArticle.TitleObj.Title.ToSlug(),
                HtmlHelper.StripHtml(wpArticle.ContentObj.Content.Substring(0, 200)),
                wpArticle.ImgSrc,
                "",
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

    public async Task<List<Article>> GetArticlesFromRssApp(string url, ArticleSiteEnum articleSite)
    {
        // List<RssApp> rssData = new List<RssApp>();
        List<Article> articles = new List<Article>();
        
        var res = await _client.GetAsync(url);

        if (!res.IsSuccessStatusCode)
        {
            return articles; 
        }
        
        string articleStr = await res.Content.ReadAsStringAsync();
         
        RssApp? rssData = JsonConvert.DeserializeObject<RssApp>(articleStr);

        if (rssData == null)
        {
            return articles;
        }

        foreach (var rssItem in rssData.Items)
        {
            Article article = new(
                rssItem.Title,
                articleSite,
                rssItem.Url,
                rssItem.Title.ToSlug(),
                rssItem.ContentText.Trim(),
                rssItem.Image ?? "",
                "",
                DateHelper.ConvertStrToDate(rssItem.DatePublished),
                new ArticleStats()
            );
            
            articles.Add(article);
        }

        return articles;
    }

    /// <summary>
    /// Get a list of articles from external websites
    /// </summary>
    /// <returns></returns>
    public async Task<List<Article>> GetExternalArticles(List<ArticleSource> sources)
    {
        object listLock = new object();
        List<Article> xmlList = new List<Article>();
        List<Article> wpList = new List<Article>();
        List<Article> rssList = new List<Article>();
        try
        {
            var getArticlesTasks = sources.Select(async source =>
            {
                switch (source.Type)
                {
                    case ArticleSourceType.Xml:
                        List<Article> articlesFromXml = await GetArticlesFromXML(source.RssUrl, source.ArticleSite);
                        if (articlesFromXml.Count > 0)
                        {
                            lock (listLock)
                            {
                                xmlList.AddRange(articlesFromXml);
                            }
                        }
                        break;
                    case ArticleSourceType.WordPressJson:
                        List<Article> articlesFromJson = await GetArticlesFromJson(source.RssUrl, source.ArticleSite);
                        if (articlesFromJson.Count > 0)
                        {
                            lock (listLock)
                            {
                                wpList.AddRange(articlesFromJson);
                            }
                        }
                        break;
                    case ArticleSourceType.RrsAppJson:
                        List<Article> articlesFromRssApp = await GetArticlesFromRssApp(source.RssUrl, source.ArticleSite);
                        
                        if (articlesFromRssApp.Count > 0)
                        {
                            lock (listLock)
                            {
                                rssList.AddRange(articlesFromRssApp);
                            }
                        }
                        break;
                    default:
                        break;
                }
            });

            await Task.WhenAll(getArticlesTasks);

            var articlesList = xmlList.Concat(wpList).Concat(rssList).ToList();
            
            return articlesList;
        }
        catch (Exception e)
        {
            //TODO log the error
            return new List<Article>();
        }
    }
}