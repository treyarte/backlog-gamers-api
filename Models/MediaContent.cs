using System.Xml.Serialization;

namespace xmlParseExample.Models;

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