using backlog_gamers_api.Models.Enums;

namespace backlog_gamers_api.Helpers;

public static class TagHelper
{
    public static readonly Dictionary<string, List<Tag>> TagKeywordMap = new()
    {
        // Genres
        { "jrpg", [Tag.RPG, Tag.JRPG] },
        { "rpg", [Tag.RPG] },
        { "soulslike", [Tag.RPG, Tag.Soulslike] },
        { "soulsborne", [Tag.RPG, Tag.Soulslike] },
        { "roguelike", [Tag.Indie, Tag.Roguelike] },
        { "roguelite", [Tag.Indie, Tag.Roguelike] },
        { "metroidvania", [Tag.Metroidvania] },
        { "horror", [Tag.Horror] },
        { "survivalhorror", [Tag.Horror] },
        { "fps", [Tag.Shooter, Tag.FPS] },
        { "shooter", [Tag.Shooter, Tag.FPS] },
        { "platformer", [Tag.Platformer] },
        { "stealth", [Tag.Stealth] },

        // Platforms
        { "ps5", [Tag.PS5] },
        { "playstation", [Tag.PS5] },
        { "xbox", [Tag.Xbox] },
        { "switch", [Tag.Nintendo] },
        { "nintendo", [Tag.Nintendo] },
        { "pc", [Tag.PC, Tag.Steam] },
        { "steam", [Tag.PC, Tag.Steam] },

        // Studios/franchises (optional)
        { "fromsoftware", [Tag.RPG, Tag.Soulslike, Tag.FromSoftware] },
        { "atlus", [Tag.JRPG, Tag.Atlus, Tag.RPG ]}
    };
} 

