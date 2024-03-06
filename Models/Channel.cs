using System.Xml.Serialization;
using xmlParseExample.Models;

namespace backlog_gamers_api.Models;

public class Channel
{
    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("link")]
    public string Link { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }

    [XmlElement("atom:link")]
    public AtomLink AtomLink { get; set; }

    [XmlElement("item")]
    public List<Item> Items { get; set; }
}