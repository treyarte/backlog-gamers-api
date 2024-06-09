using backlog_gamers_api.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using xmlParseExample.Models.Enums;

namespace backlog_gamers_api.Models.Articles;

/// <summary>
/// Article class that holds news information from external and internal sites
/// </summary>
public class Article : BaseMongoModel
{
    public Article()
    {
        Title = "";
        Url = "";
        ArticleSite = ArticleSiteEnum.Unknown;
        ShortDescription = "";
        ImageUrl = "";
        Content = "";
        ArticleDate = DateTimeOffset.MinValue;
        Stats = new ArticleStats();
    }
    public Article(
        string title,
        ArticleSiteEnum articleSite,
        string url,
        string shortDescription,
        string imageUrl,
        string content,
        DateTimeOffset articleDate,
        ArticleStats stats
        )
    {
        Title = title;
        ArticleSite = articleSite;
        Url = url;
        ShortDescription = shortDescription;
        ImageUrl = imageUrl;
        Content = content;
        ArticleDate = articleDate;
        Stats = stats;
    }
    public string Title { get; set; }
    public ArticleSiteEnum ArticleSite { get; set; }
    public string Url { get; set; }
    public string ShortDescription { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public ArticleStats Stats { get; set; }
    
    [BsonSerializer(typeof(CustomDateTimeOffsetSerializer))]
    public DateTimeOffset ArticleDate { get; set; }
    
    public List<MongoIdObject> Tags { get; set; }
}

/// <summary>
/// Statical data about an individual article
/// </summary>
public class ArticleStats
{   
    public int Likes { get; set; }
    public int Shares { get; set; }
    public int CommentTotal { get; set; }
    public int Clicks { get; set; }

    public ArticleStats()
    {
        this.Likes = 0;
        this.Shares = 0;
        this.CommentTotal = 0;
        this.Clicks = 0;
    }

    public ArticleStats(
        int likes,
        int shares,
        int commentTotal,
        int clicks)
    {
        this.Likes = likes;
        this.Shares = shares;
        this.CommentTotal = commentTotal;
        this.Clicks = clicks;
    }
}