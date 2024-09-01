using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace backlog_gamers_api.Models.Articles;

/// <summary>
/// A tqg object to match an article with a keyword
/// </summary>
public class ArticleTag : BaseMongoModel
{
    public ArticleTag(string displayName, string slug, string name, List<string> alternativeNames)
    {
        Name = name;
        DisplayName = displayName;
        Slug = slug;
        AlternativeNames = alternativeNames;
    }

    public ArticleTag()
    {
        Name = "";
        DisplayName = "";
        Slug = "";
        AlternativeNames = new List<string>();
    }
    
    [JsonProperty("name")]
    [BsonElement("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// A user friendly name
    /// </summary>
    [JsonProperty("displayName")]
    [BsonElement("displayName")]
    public string DisplayName { get; set; }
    /// <summary>
    /// A slug/path for the tag
    /// </summary>
    [JsonProperty("slug")]
    [BsonElement("slug")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Slug { get; set; }
    
    /// <summary>
    /// Other names the tag can take such as Nintendo: Switch, Wii, Wii U, etc
    /// </summary>
    [JsonProperty("alternativeNames")]
    [BsonElement("alternativeNames")]
    public List<string> AlternativeNames { get; set; }
}