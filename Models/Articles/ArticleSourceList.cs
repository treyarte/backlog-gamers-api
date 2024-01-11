using xmlParseExample.Models.Enums;

namespace xmlParseExample.Models;

public static class ArticleSourceList
{
    public static List<ArticleSource> Sources { get; set; } = new List<ArticleSource>()
    {
        new ArticleSource(
            NewsSource.Ign,
            "https://www.ign.com",
            "http://feeds.feedburner.com/ign/games-all",
            "https://www.ign.com/favicon.ico"
        ),

        new ArticleSource(
            NewsSource.GameSpot,
            "https://www.gamespot.com/",
            "http://feeds.feedburner.com/ign/games-all",
            "https://www.gamespot.com/apple-touch-icon.png"
        ),
    };
}