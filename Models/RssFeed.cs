using System.Threading.Channels;
using System.Xml.Serialization;

namespace xmlParseExample.Models;

[Serializable]
[XmlRoot("rss", Namespace = "")]
public class RssFeed
{
    [XmlElement("channel")]
    public Channel Channel { get; set; }
}