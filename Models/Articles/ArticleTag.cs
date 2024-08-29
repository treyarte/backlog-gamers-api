namespace backlog_gamers_api.Models.Articles;

/// <summary>
/// A tqg object to match an article with a keyword
/// </summary>
public class ArticleTag : BaseMongoModel
{
    public ArticleTag(string displayName, string slug)
    {
        DisplayName = displayName;
        Slug = slug;
    }

    public ArticleTag()
    {
        DisplayName = "";
        Slug = "";
    }

    /// <summary>
    /// A user friendly name
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// A slug/path for the tag
    /// </summary>
    public string Slug { get; set; }
}