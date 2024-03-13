using System.Xml.Serialization;

namespace backlog_gamers_api.Models;

public class MediaContent
{
    [XmlAttribute("height")]
    public int Height { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName="url")]
    public string Url { get; set; }

    [XmlAttribute("width")]
    public int Width { get; set; }
}

/// <summary>
/// Some xml files use this format instead of mediaContent
/// </summary>
// public class MediaThumbnail 