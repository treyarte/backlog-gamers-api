using System.Xml.Serialization;
using backlog_gamers_api.Models;

namespace xmlParseExample.Models;

public class Item
{
    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("link")]
    public string Link { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }

    [XmlElement("pubDate")]
    public string PubDate { get; set; }

    [XmlElement("guid")]
    public GuidElement Guid { get; set; }

    [XmlElement("encoded", Namespace = "http://purl.org/rss/1.0/modules/content/")]
    public string ContentEncoded { get; set; }

    [XmlElement("content", Namespace = "http://search.yahoo.com/mrss/") ]
    public MediaContent MediaContent { get; set; }
    
    [XmlElement("thumbnail", Namespace = "http://search.yahoo.com/mrss/") ]
    public MediaContent Media { get; set; }

    [XmlElement("creator", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string DcCreator { get; set; }
}