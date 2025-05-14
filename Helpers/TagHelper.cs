using backlog_gamers_api.Models.Enums;

namespace backlog_gamers_api.Helpers;

public static class TagHelper
{
    public static readonly Dictionary<string, List<Tag>> TagKeywordMap = new()
    {
        // Genres
        { "jrpg", [Tag.RPG, Tag.JRPG] },
        { "rpg", [Tag.RPG] },
        { "role playing", [Tag.RPG] },
        { "turn based", [Tag.RPG]},
        { "soulslike", [Tag.RPG, Tag.Soulslike, Tag.FromSoftware] },
        { "soulsborne", [Tag.RPG, Tag.Soulslike] },
        { "roguelike", [Tag.Roguelike] },
        { "roguelite", [Tag.Roguelike] },
        { "metroidvania", [Tag.Metroidvania] },
        { "horror", [Tag.Horror] },
        { "survivalhorror", [Tag.Horror] },
        { "fps", [Tag.Shooter, Tag.FPS] },
        { "shooter", [Tag.Shooter, Tag.FPS] },
        { "platformer", [Tag.Platformer] },
        { "stealth", [Tag.Stealth] },
        { "indie", [Tag.Indie]},
        { "esports", [Tag.ESports]},
        {"arena shooter", [Tag.ArenaShooter]},
        {"live service", [Tag.LiveService]},
        {"mmo", [Tag.MMO]},
        {"mmorpg", [Tag.MMO]},
        {"massive multiplayer", [Tag.MMO]},
        

        // Platforms
        { "ps5", [Tag.PS5, Tag.Playstation] },
        { "playstation", [Tag.PS5, Tag.Playstation] },
        { "xbox", [Tag.Xbox] },
        { "switch", [Tag.Nintendo] },
        { "nintendo", [Tag.Nintendo] },
        { "pc", [Tag.PC, Tag.Steam] },
        { "steam", [Tag.PC, Tag.Steam] },

        // Studios/franchises
        { "fromsoftware", [Tag.RPG, Tag.Soulslike, Tag.FromSoftware] },
        { "atlus", [Tag.JRPG, Tag.Atlus, Tag.RPG ]},
        { "metriod", [Tag.Metroidvania, Tag.Nintendo, Tag.Metriod ]},
        //{ "review", [Tag.Reviews]},
        
        //Events
        { "nintendo direct", [Tag.Nintendo, Tag.NintendoDirect] },
    };

    public static List<Tag> GetTags(string title, string summary)
    {
        try
        {
            string text = (title + "\n" + summary).ToLowerInvariant();
            text = Normalize(text);
            HashSet<Tag> tags = [];
            foreach (var tag in TagKeywordMap)
            {
                if (text.Contains(tag.Key))
                {
                    tags.UnionWith(tag.Value);
                }
            }
            return tags.ToList();
        }
        catch (Exception e)
        {
            //Default tags if any
            return new List<Tag>();
        }
    }
    
    
    public static string Normalize(string input)
    {
        return input
            .ToLowerInvariant()
            .Replace("-", "") // remove dashes
            .Replace("’", "") // smart apostrophes
            .Replace("'", "") // straight apostrophes
            .Replace("\"", "")
            .Replace(",", "")
            .Replace(".", "");
    }
} 

