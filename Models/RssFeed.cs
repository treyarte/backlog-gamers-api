using System.Threading.Channels;
using System.Xml.Serialization;
using Channel = backlog_gamers_api.Models.Channel;

namespace xmlParseExample.Models;

[Serializable]
[XmlRoot("rss", Namespace = "")]
public class RssFeed
{
    [XmlElement("channel")]
    public Channel Channel { get; set; }
}