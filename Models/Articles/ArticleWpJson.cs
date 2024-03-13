using Newtonsoft.Json;

namespace xmlParseExample.Models;

/// <summary>
/// used to deserialize JSON articles from word press
/// </summary>
/// <remarks>
/// TODO check for published/status
/// </remarks>
public class ArticleWpJson
{
    [JsonProperty("date_gmt")]
    public string DateString { get; set; }
    [JsonProperty("slug")]
    public string Slug { get; set; }
    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("title")]
    public TitleWpJson TitleObj { get; set; }
    [JsonProperty("content")] 
    public ContentWpJson ContentObj { get; set; }
    [JsonProperty("link")]
    public string Link { get; set; }
    [JsonProperty("jetpack_featured_media_url")]
    public string ImgSrc { get; set; }
    [JsonProperty("yoast_head_json")]
    public YoastJson Yoast { get; set; }
}

/// <summary>
/// Title of the article
/// </summary>
public class TitleWpJson
{
    [JsonProperty("rendered")]
    public string Title { get; set; }
}

/// <summary>
/// content of the article
/// </summary>
public class ContentWpJson
{
    [JsonProperty("rendered")]
    public string Content { get; set; }
}


/// <summary>
/// Json from seo Yoast
/// </summary>
public class YoastJson
{
    [JsonProperty("og_image")]
    public List<OgImage> ImgSrcTwo { get; set; }
}

/// <summary>
/// A backup in case the json uses a different image property name
/// </summary>
public class OgImage
{
    [JsonProperty("width")]
    public int Width { get; set; }
    [JsonProperty("height")]
    public int Height { get; set; }
    [JsonProperty("url")]
    public string ImgUrl { get; set; }
    [JsonProperty("type")]
    public string Type { get; set; }
}