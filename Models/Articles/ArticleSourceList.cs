using xmlParseExample.Models.Enums;

namespace xmlParseExample.Models;

public static class ArticleSourceList
{
    public static List<ArticleSource> Sources { get; set; } = new List<ArticleSource>()
    {
        new ArticleSource(            
            "IGN",
            ArticleSourceType.Xml,
            ArticleSiteEnum.Ign,
            "https://www.ign.com",
            "http://feeds.feedburner.com/ign/games-all",
            "https://www.ign.com/favicon.ico"
        ),
        
        new ArticleSource(            
            "GameSpot",
            ArticleSourceType.RrsAppJson,
            ArticleSiteEnum.GameSpot,
            "https://www.gamespot.com/",
            "https://rss.app/feeds/v1.1/mv89HRMuIahF3oli.json",
            "https://www.gamespot.com/apple-touch-icon.png"
        ),
        
        new ArticleSource(            
            "VG247",
            ArticleSourceType.RrsAppJson,
            ArticleSiteEnum.Vg247,
            "https://www.vg247.com/",
            "https://rss.app/feeds/v1.1/2Pz83wU22bSnNzv9.json",
            "https://assetsio.reedpopcdn.com/0502_vg247-logo-og.png?width=1200&height=600&fit=crop&enable=upscale&auto=webp"
        ),
        new ArticleSource(            
            "PCGamer",
            ArticleSourceType.RrsAppJson,
            ArticleSiteEnum.PcGamer,
            "https://www.pcgamer.com",
            "https://rss.app/feeds/v1.1/7SIFm6ySYzJh5paD.json",
            "https://forums.pcgamer.com/data/avatars/l/15/15548.jpg"
        ),
        new ArticleSource(            
            "Nintendo Life",
            ArticleSourceType.RrsAppJson,
            ArticleSiteEnum.NintendoLife,
            "https://www.nintendolife.com/",
            "https://rss.app/feeds/v1.1/68MPDj96O2DazLOp.json",
            "https://images.nintendolife.com/site/logo.svg?colour=e60012"
        ),
        new ArticleSource(            
            "Kotaku",
            ArticleSourceType.RrsAppJson,
            ArticleSiteEnum.Kotaku,
            "https://kotaku.com",
            "https://rss.app/feeds/v1.1/RBMEJ9Y0dgjGEu4r.json",
            ""
        ),
        new ArticleSource(            
            "GameDeveloper.com",
            ArticleSourceType.Xml,
            ArticleSiteEnum.GameDeveloper,
            "https://www.gamedeveloper.com",
            "https://www.gamedeveloper.com/rss.xml",
            ""
        ),
        new ArticleSource(            
            "Hardcore Gamer",
            ArticleSourceType.RrsAppJson,
            ArticleSiteEnum.HardcoreGamer,
            "https://hardcoregamer.com/",
            "https://rss.app/feeds/v1.1/wpv1AEiTLiyN5J4A.json",
            ""
        ),
        new ArticleSource(            
            "Dual Shockers",
            ArticleSourceType.RrsAppJson,
            ArticleSiteEnum.DualShockers,
            "https://www.dualshockers.com/",
            "https://rss.app/feeds/v1.1/4uUb1eHLIgksxpt3.json",
            ""
        ),
        new ArticleSource(            
            "Gematsu",
            ArticleSourceType.WordPressJson,
            ArticleSiteEnum.Gematsu,
            "https://www.gematsu.com",
            "https://www.gematsu.com/wp-json/wp/v2/posts",
            // "https://gematsu.com/feed",
            ""
        ),
        new ArticleSource(            
            "Desctructiod",
            ArticleSourceType.WordPressJson,
            ArticleSiteEnum.Desctructiod,
            "https://www.destructoid.com",
            "https://www.destructoid.com/wp-json/wp/v2/posts",
            ""
        ),
        new ArticleSource(            
            "MMOs.com",
            ArticleSourceType.WordPressJson,
            ArticleSiteEnum.Mmos,
            "https://mmos.com",
            "https://mmos.com/wp-json/wp/v2/posts",
            ""
        ),
        new ArticleSource(            
            "Siliconera",
            ArticleSourceType.WordPressJson,
            ArticleSiteEnum.Siliconera,
            "https://www.siliconera.com",
            "https://www.siliconera.com/wp-json/wp/v2/posts",
            ""
        ),
    };
}