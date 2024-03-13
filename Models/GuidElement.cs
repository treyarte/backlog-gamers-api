using System.Xml.Serialization;

namespace backlog_gamers_api.Models;

public class GuidElement
{
    [XmlAttribute("isPermaLink")]
    public bool IsPermaLink { get; set; }
}