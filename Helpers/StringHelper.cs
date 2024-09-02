using System.Text.RegularExpressions;

namespace backlog_gamers_api.Helpers;

public static class StringHelper
{
    /// <summary>
    /// Check if a slug is valid or not
    /// </summary>
    /// <param name="slug"></param>
    /// <returns></returns>
    public static bool CheckIfValidSlug(string slug)
    {
        string pattern = @"^[a-z0-9]+(?:-[a-z0-9]+)*$";

        try
        {
            Regex regex = new Regex(pattern);

            return regex.IsMatch(slug);
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    /// <summary>
    /// Remove special characters (except "-") from a url slug
    /// </summary>
    /// <param name="slug"></param>
    /// <returns></returns>
    public static string SanitizeSlug(string slug)
    {
        string pattern = @"[^a-z0-9\-]+";
        Regex regex = new Regex(pattern);
        slug = slug.Trim().ToLower();
        slug = regex.Replace(slug, "");
        return slug;
    }
}