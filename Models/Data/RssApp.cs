using Newtonsoft.Json;

namespace backlog_gamers_api.Models.Data;

/// <summary>
/// Data format that comes back from RSS.App
/// </summary>
public class RssApp
{
    [JsonProperty("version")]
    public string Version { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; }
    [JsonProperty("home_page_url")]
    public string HomePageUrl { get; set; }
    [JsonProperty("feed_url")]
    public string FeedUrl { get; set; }
    [JsonProperty("language")]
    public string Language { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("items")]
    public List<RssAppItem> Items { get; set; }
}

/// <summary>
/// Holds article content
/// </summary>
public class RssAppItem
{
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("url")] public string Url { get; set; }
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("content_text")] public string ContentText { get; set; }
    [JsonProperty("content_html")] public string ContentHtml { get; set; }
    [JsonProperty("image")] public string Image { get; set; }
    [JsonProperty("date_published")] public string DatePublished { get; set; }
    [JsonProperty("authors")] public List<RssAppAuthor> Authors { get; set; }
    [JsonProperty("attachments")] public List<RssAppAttachment> Attachments { get; set; }
}

/// <summary>
/// Name of the author in the Rss App
/// </summary>
public class RssAppAuthor
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

/// <summary>
/// Attachments such as images
/// </summary>
public class RssAppAttachment
{
    [JsonProperty("url")]
    public string Url { get; set; }
}