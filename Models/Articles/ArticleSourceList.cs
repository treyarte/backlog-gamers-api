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
            ArticleSourceType.Xml,
            ArticleSiteEnum.GameSpot,
            "https://www.gamespot.com/",
            "http://feeds.feedburner.com/ign/games-all",
            "https://www.gamespot.com/apple-touch-icon.png"
        ),
        
        new ArticleSource(            
            "VG247",
            ArticleSourceType.Xml,
            ArticleSiteEnum.Vg247,
            "https://www.vg247.com/",
            "https://www.vg247.com/feed/articles",
            "https://assetsio.reedpopcdn.com/0502_vg247-logo-og.png?width=1200&height=600&fit=crop&enable=upscale&auto=webp"
        ),
        
        new ArticleSource(            
            "VG247",
            ArticleSourceType.Xml,
            ArticleSiteEnum.Vg247,
            "https://www.vg247.com/",
            "https://www.vg247.com/feed/news",
            "https://assetsio.reedpopcdn.com/0502_vg247-logo-og.png?width=1200&height=600&fit=crop&enable=upscale&auto=webp"
        ),
        new ArticleSource(            
            "PCGamer",
            ArticleSourceType.Xml,
            ArticleSiteEnum.PcGamer,
            "https://www.pcgamer.com",
            "https://www.pcgamer.com/rss",
            "https://forums.pcgamer.com/data/avatars/l/15/15548.jpg"
        ),
        new ArticleSource(            
            "Nintendo Life",
            ArticleSourceType.Xml,
            ArticleSiteEnum.NintendoLife,
            "https://www.nintendolife.com/",
            "https://www.nintendolife.com/feeds/news",
            "https://images.nintendolife.com/site/logo.svg?colour=e60012"
        ),
        new ArticleSource(            
            "Nintendo Life",
            ArticleSourceType.Xml,
            ArticleSiteEnum.NintendoLife,
            "https://www.nintendolife.com/",
            "https://www.nintendolife.com/feeds/latest",
            "https://images.nintendolife.com/site/logo.svg?colour=e60012"
        ),
        new ArticleSource(            
            "Kotaku",
            ArticleSourceType.Xml,
            ArticleSiteEnum.Kotaku,
            "https://kotaku.com",
            "https://kotaku.com/rss",
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
            "Gematsu",
            ArticleSourceType.WordPressJson,
            ArticleSiteEnum.Gematsu,
            "https://www.gematsu.com",
            "https://www.gematsu.com/wp-json/wp/v2/posts",
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
            "https://mmos.com/wp-json",
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