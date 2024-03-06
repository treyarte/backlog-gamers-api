using xmlParseExample.Models.Enums;

namespace xmlParseExample.Models;

public class ArticleSource
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

    public string Title { get; set; }
    
    public ArticleSourceType Type { get; set; }

    public ArticleSiteEnum ArticleSite { get; set; }
    
    /// <summary>
    /// The url of the website 
    /// </summary>
    public string SiteUrl { get; set; }
    
    /// <summary>
    /// Url of the rss feed that was used
    /// </summary>
    public string RssUrl { get; set; }
    
    /// <summary>
    /// Logo image link of the site
    /// </summary>
    public string LogoSrc { get; set; }
}