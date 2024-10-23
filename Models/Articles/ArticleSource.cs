using backlog_gamers_api.Models;
using MongoDB.Bson.Serialization.Attributes;
using xmlParseExample.Models.Enums;

namespace xmlParseExample.Models;

[BsonIgnoreExtraElements]
public class ArticleSource : BaseMongoModel
{

    public ArticleSource(string title, ArticleSourceType type, ArticleSiteEnum articleSite,
        string siteUrl, string rssUrl, string logoSrc)
    {
        Title = title;
        Type = type;
        ArticleSite = articleSite;
        SiteUrl = siteUrl;
        RssUrl = rssUrl;
        LogoSrc = logoSrc;
    }

    [BsonElement("title")]
    public string Title { get; set; }
    
    [BsonElement("type")]
    public ArticleSourceType Type { get; set; }
    
    [BsonElement("articleSite")]
    public ArticleSiteEnum ArticleSite { get; set; }
    
    /// <summary>
    /// The url of the website 
    /// </summary>
    [BsonElement("siteUrl")]
    public string SiteUrl { get; set; }
    
    /// <summary>
    /// Url of the rss feed that was used
    /// </summary>
    [BsonElement("rssUrl")]
    public string RssUrl { get; set; }
    
    /// <summary>
    /// Logo image link of the site
    /// </summary>
    [BsonElement("logoSrc")]
    public string LogoSrc { get; set; }
}