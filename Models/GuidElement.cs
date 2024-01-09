using System.Xml.Serialization;

namespace xmlParseExample.Models;

public class GuidElement
{
    [XmlAttribute("isPermaLink")]
    public bool IsPermaLink { get; set; }
}