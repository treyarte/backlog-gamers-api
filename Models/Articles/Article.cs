namespace backlog_gamers_api.Models.Articles;

/// <summary>
/// Article class that holds news information from external and internal sites
/// </summary>
public class Article : BaseMongoModel
{
    public Article()
    {
        this.Title = "";
        this.Url = "";
        this.ShortDescription = "";
        this.ImageUrl = "";
        this.Content = "";
        this.ArticleDate = null;
        this.Stats = new ArticleStats();
    }
    public Article(
        string title,
        string url,
        string shortDescription,
        string imageUrl,
        string content,
        DateTimeOffset? articleDate,
        ArticleStats stats
        )
    {
        this.Title = title;
        this.Url = url;
        this.ShortDescription = shortDescription;
        this.ImageUrl = imageUrl;
        this.Content = content;
        this.ArticleDate = articleDate;
        this.Stats = stats;
    }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ShortDescription { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public ArticleStats Stats { get; set; }
    public DateTimeOffset? ArticleDate { get; set; }
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