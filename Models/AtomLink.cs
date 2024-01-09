using System.Xml.Serialization;

namespace xmlParseExample.Models;

public class AtomLink
{
    [XmlAttribute("href")]
    public string Href { get; set; }

    [XmlAttribute("rel")]
    public string Rel { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }
}