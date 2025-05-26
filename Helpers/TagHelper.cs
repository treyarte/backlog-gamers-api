using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Models.Enums;

namespace backlog_gamers_api.Helpers;

public static class TagHelper
{
    /// <summary>
    /// List of pontial tags possible in the system
    /// </summary>
    public static readonly Dictionary<string, List<Tag>> TagKeywordMap = new()
    {
        // ─── Genres ───────────────────────────────────────────────────────────────────────
        { "rpg", new() { Tag.RPG } },
        { "jrpgs", [Tag.RPG, Tag.JRPG, Tag.Japanese] },
        { "jrpg", new() { Tag.JRPG, Tag.RPG, Tag.Japanese } },
        { "turn-based", [Tag.RPG] },
        { "leveling", [Tag.RPG] },
        { "role playing",    new() { Tag.RPG } },
        { "soulslike",       new() { Tag.Soulslike, Tag.RPG, Tag.FromSoftware } },
        { "soulsborne",      new() { Tag.Soulslike, Tag.RPG } },
        { "roguelike",       new() { Tag.Roguelike } },
        { "roguelite",       new() { Tag.Roguelike } },
        { "permadeath",      new() { Tag.Roguelike } },
        { "metroidvania",    new() { Tag.Metroidvania, Tag.Platformer } },
        { "platformer",      new() { Tag.Platformer } },
        { "side scroller",   new() { Tag.Platformer } },
        { "stealth",         new() { Tag.Stealth } },
        { "horror",          new() { Tag.Horror } },
        { "survival horror", new() { Tag.Horror } },
        { "jump scare",      new() { Tag.Horror } },
        { "shooter",         new() { Tag.Shooter } },
        { "fps",             new() { Tag.FPS, Tag.Shooter } },
        { "first person",    new() { Tag.FPS, Tag.Shooter } },
        { "third person",    new() { Tag.ThirdPersonShooter, Tag.Shooter } },
        { "arena shooter",   new() { Tag.ArenaShooter, Tag.Shooter } },
        { "mmo",             new() { Tag.MMO } },
        { "mmorpg",          new() { Tag.MMORPG } },
        { "massive multiplayer", new() { Tag.MMO, Tag.MMORPG } },
        { "coop",            new() { Tag.Coop, Tag.Multiplayer } },
        { "co-op",           new() { Tag.Coop, Tag.Multiplayer } },
        { "co op",           new() { Tag.Coop, Tag.Multiplayer } },
        { "multiplayer",     new() { Tag.Multiplayer } },
        { "fighting",        new() { Tag.Fighting } },
        { "simulation",      new() { Tag.Simulation } },
        { " sim ",             new() { Tag.Simulation } },
        { "survival",        new() { Tag.Survival } },
        { "open world",      new() { Tag.OpenWorld } },
        { "shmup",           new() { Tag.Shmup } },
        { "bullet hell",     new() { Tag.BulletHell, Tag.Shmup } },
        { "deckbuilder",     new() { Tag.Deckbuilder } },
        { "indie",           new() { Tag.Indie } },
        { "puzzle",          new() { Tag.Puzzle } },
        { "visual novel",    new() { Tag.VisualNovels } },
        { "visualnovel",     new() { Tag.VisualNovels } },
        { "action",          new() { Tag.Action } },

        // ─── Companies ────────────────────────────────────────────────────────────────────
        { "fromsoftware",    new() { Tag.FromSoftware, Tag.RPG, Tag.Soulslike, Tag.Japanese } },
        { "atlus",           new() { Tag.Atlus, Tag.JRPG, Tag.RPG, Tag.Japanese } },
        { "riot games",      new() { Tag.RiotGames, Tag.MMO } },
        { "riot",            new() { Tag.RiotGames } },
        { "nintendo",        new() { Tag.Nintendo, Tag.Japanese } },
        { "playstation",     new() { Tag.Playstation, Tag.PS5 } },
        { "bandai namco",    new() { Tag.BandaiNamco, Tag.Japanese } },
        { "wayforward",      new() { Tag.WayForward } },
        { "bethesda",        new() { Tag.Bethesda } },
        { "level5",          new() { Tag.Level5, Tag.Japanese } },
        { "level 5",         new() { Tag.Level5, Tag.Japanese } },
        { "sega",            new() { Tag.Sega, Tag.Japanese } },
        { "netherrealm",     new() { Tag.NetherRealm } },
        { "konami",          new() { Tag.Konami } },
        { "square enix",     new() { Tag.SquareEnix, Tag.Japanese, Tag.JRPG, Tag.RPG} },
        { "cd projekt red",  new() { Tag.CDProjektRed } },
        { "falcom",          new() { Tag.NihonFalcom, Tag.Japanese } },
        { "nihon falcom",    new() { Tag.NihonFalcom, Tag.Japanese } },
        { " nis ",             new() { Tag.NIS, Tag.Japanese, Tag.JRPG, Tag.RPG } },
        { " nisa ",            new() { Tag.NIS, Tag.Japanese, Tag.JRPG, Tag.RPG } },
        { "nis america",     new() { Tag.NIS, Tag.Japanese, Tag.JRPG, Tag.RPG } },
        { "monolith soft",   new() { Tag.MonolithSoft, Tag.Japanese } },
        { "capcom",          new() { Tag.Capcom, Tag.Japanese } },
        { "activision blizzard",        new() { Tag.Blizzard } },
        { " ea ",              new() { Tag.EA } },

        // ─── Consoles & Platforms ─────────────────────────────────────────────────────────
        { "pc",              new() { Tag.PC, Tag.Steam } },
        { "steam",           new() { Tag.Steam, Tag.PC } },
        { "ps5",             new() { Tag.PS5, Tag.Playstation } },
        { "xbox",            new() { Tag.Xbox } },
        { "mobile",          new() { Tag.Mobile } },
        { "switch 2",        new() { Tag.Switch2, Tag.Nintendo, Tag.Japanese } },
        { "switch",          new() { Tag.Switch, Tag.Nintendo, Tag.Japanese } },

        // ─── Events ───────────────────────────────────────────────────────────────────────
        { "nintendo direct", new() { Tag.NintendoDirect, Tag.Japanese, Tag.Nintendo } },
        { "otk games expo",  new() { Tag.OTKGamesExpo } },
        { " pax ",             new() { Tag.PAX } },
        { "gamescom",        new() { Tag.Gamescom } },

        // ─── Payments & Monetization ───────────────────────────────────────────────────────
        { "xbox game pass",  new() { Tag.XboxGamePass } },
        { "game pass",       new() { Tag.XboxGamePass } },
        { "live service",    new() { Tag.LiveService } },
        { "gacha",           new() { Tag.Gacha } },
        { "free to play",    new() { Tag.FreeToPlay } },
        { "freemium",        new() { Tag.FreeToPlay } },

        // ─── Major Game Titles ────────────────────────────────────────────────────────────
        { "elden ring",      new() { Tag.EldenRing, Tag.RPG, Tag.Soulslike, Tag.FromSoftware } },
        { "metroid",         new() { Tag.Metriod, Tag.Metroidvania, Tag.Nintendo } },
        { "league of legends", new() { Tag.LeagueOfLegends, Tag.ESports, Tag.MMO } },
        { "lol", [Tag.LeagueOfLegends] },
        { "league", [Tag.LeagueOfLegends] },
        { "dragon ball z",     new() { Tag.DragonBallZ, Tag.Fighting, Tag.Japanese } },
        { "dbz", [Tag.DragonBallZ, Tag.Fighting, Tag.Japanese] },
        { "dragon ball", [Tag.DragonBallZ, Tag.Fighting] },
        { "doom",            new() { Tag.Doom, Tag.Shooter, Tag.FPS } },
        { "mario kart",      new() {Tag.Mario, Tag.MarioKart, Tag.MarioKartWorld } },
        { "mario",           new() { Tag.Mario } },
        { "sonic",           new() { Tag.Sonic } },
        { "pokemon",         new() { Tag.Pokemon } },
        { "pokemon za",      new() { Tag.PokemonZA } },
        { "mortal kombat",   new() { Tag.MortalKombat, Tag.Fighting } },
        { " persona ",         new() { Tag.Persona, Tag.JRPG, Tag.RPG, Tag.Atlus } },
        { "dragon quest",    new() { Tag.DragonQuest, Tag.RPG, Tag.SquareEnix } },
        { "final fantasy",   new() { Tag.FinalFantasy, Tag.RPG, Tag.SquareEnix, Tag.JRPG, Tag.Japanese } },
        { " ff ", [Tag.FinalFantasy, Tag.SquareEnix, Tag.Japanese] },
        { "ff7", [Tag.FinalFantasy, Tag.Remake, Tag.SquareEnix, Tag.Japanese] },
        { "street fighter",  new() { Tag.StreetFighter, Tag.Fighting } },
        { "overwatch 2",     new() { Tag.Overwatch2, Tag.Shooter } },
        { "donkey kong",     new() { Tag.DonkeyKong, Tag.Platformer } },
        { "trails series",   new() { Tag.TrailsSeries, Tag.RPG, Tag.NihonFalcom, Tag.Japanese } },
        { "nier",            new() { Tag.Nier, Tag.RPG, Tag.Japanese, Tag.JRPG } },
        { " ys ",              new() { Tag.Ys, Tag.RPG, Tag.JRPG, Tag.Action, Tag.Japanese, Tag.NihonFalcom } },
        { "warhammer",       new() { Tag.Warhammer } },
        { "fortnite",        new() { Tag.Fortnite, Tag.ArenaShooter,Tag.BattleRoyale, Tag.Shooter } },
        { "silent hill",     new() { Tag.SilentHill, Tag.Horror } },
        { "stellar blade",   new() { Tag.StellarBlade } },
        { "zenless zone zero", new() { Tag.ZenlessZoneZero, Tag.Gacha, Tag.FreeToPlay } },
        { "genshin impact",  new() { Tag.GenshinImpact, Tag.Gacha, Tag.FreeToPlay } },
        { "honkai star rail", new() { Tag.HonkaiStarRail, Tag.Gacha, Tag.FreeToPlay } },
        { "honkai impact",   new() { Tag.HonkaiImpact, Tag.Gacha, Tag.FreeToPlay } },
        { "monster hunter",  new() { Tag.MonsterHunter } },
        { "goddess of victory nikke", new() { Tag.GoddessOfVictoryNikke, Tag.Gacha, Tag.FreeToPlay } },
        { "infinite nikke",  new() { Tag.InfiniteNikke, Tag.Gacha, Tag.FreeToPlay } },
        { " mk ", [Tag.MortalKombat] },
        { "persona 5", [Tag.Persona, Tag.JRPG, Tag.RPG, Tag.Japanese, Tag.Atlus] },
        { " dq ", [Tag.DragonQuest, Tag.JRPG, Tag.RPG, Tag.Japanese, Tag.SquareEnix] },
        { "sf6", [Tag.StreetFighter, Tag.Capcom] },
        { " mh ", [Tag.MonsterHunter, Tag.Capcom] },
        { "nikke", [Tag.GoddessOfVictoryNikke, Tag.InfiniteNikke] },
        { "genshin", [Tag.GenshinImpact, Tag.Gacha, Tag.FreeToPlay] },
        { "honkai", [Tag.HonkaiStarRail, Tag.HonkaiImpact, Tag.Gacha] },

        // ─── News Types ──────────────────────────────────────────────────────────────────
        { "esports",         new() { Tag.ESports } },
        { " review ",         new() { Tag.Reviews } },
        { "leaks",           new() { Tag.Leaks } },
        { " trailer ",         new() { Tag.Trailers } },
        { "game announce",   new() { Tag.GameAnnounce } },
        { "interview",       new() { Tag.Interview } },
        { "rumour",          new() { Tag.Rumours } },
        { "rumours",         new() { Tag.Rumours } },

        // ─── Region / Misc ──────────────────────────────────────────────────────────────
        { "japanese",        new() { Tag.Japanese } },
        { "remake",          new() { Tag.Remake } },
        { "remastered",      new() { Tag.Remaster } },
        { "remaster",        new() { Tag.Remaster } },
    };

    /// <summary>
    /// Get tags from a title and summary text 
    /// and uses a hashset to prevent duplicate tags
    /// </summary>
    /// <param name="title"></param>
    /// <param name="summary"></param>
    /// <returns></returns>
    public static List<Tag> GetTags(string title, string summary)
    {
        try
        {
            string text = (title + "\n" + summary).ToLowerInvariant();
            text = StringHelper.Normalize(text);
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
            Console.WriteLine(e.Message.ToString());
            //Default tags if any
            return new List<Tag>();
        }
    }
    

    /// <summary>
    /// Takes a list of articles and updates their tags
    /// </summary>
    /// <param name="articles"></param>
    /// <returns></returns>
    public static List<Article> TagArticles(List<Article> articles)
    {

        foreach (var article in articles)
        {
            var tags = TagHelper.GetTags(article.Title, article.Content);
            article.Tags = tags;
        }

        return articles;
    }
} 

