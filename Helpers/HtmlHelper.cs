using HtmlAgilityPack;

namespace backlog_gamers_api.Helpers;

public class HtmlHelper
{
    public static string StripHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return "";
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        return doc.DocumentNode.InnerHtml;
    } 
}