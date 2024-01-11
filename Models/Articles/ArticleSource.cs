using xmlParseExample.Models.Enums;

namespace xmlParseExample.Models;

public class ArticleSource
{

    public ArticleSource(NewsSource source, string siteUrl, string rssUrl, string logoSrc)
    {
        Source = source;
        SiteUrl = siteUrl;
        RssUrl = rssUrl;
        LogoSrc = logoSrc;
    }
    
    /// <summary>
    /// Where the article came from
    /// </summary>
    public NewsSource Source { get; set; }
    
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