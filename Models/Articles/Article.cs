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
        Slug = "";
        ArticleSite = ArticleSiteEnum.Unknown;
        ShortDescription = "";
        ImageUrl = "";
        Content = "";
        ArticleDate = DateTimeOffset.MinValue;
        
    }
    public Article(
        string title,
        ArticleSiteEnum articleSite,
        string url,
        string slug,
        string shortDescription,
        string imageUrl,
        string content,
        DateTimeOffset articleDate)
    {
        Title = title;
        ArticleSite = articleSite;
        Url = url;
        Slug = slug;
        ShortDescription = shortDescription;
        ImageUrl = imageUrl;
        Content = content;
        ArticleDate = articleDate;
        
    }
    [BsonElement("title")]
    public string Title { get; set; }
    [BsonElement("articleSite")]
    public ArticleSiteEnum ArticleSite { get; set; }
    [BsonElement("url")]
    
    public string Url { get; set; }
    
    [BsonElement("slug")]
    public string Slug { get; set; }
    
    [BsonElement("shortDescription")]
    public string ShortDescription { get; set; }
    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; }
    [BsonElement("content")]
    public string Content { get; set; }
    [BsonElement("stats")]
    public ArticleStats Stats { get; set; }
    
    [BsonSerializer(typeof(CustomDateTimeOffsetSerializer))]
    [BsonElement("articleDate")]
    public DateTimeOffset ArticleDate { get; set; }
    [BsonElement("tags")]
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