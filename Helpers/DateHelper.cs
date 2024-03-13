namespace backlog_gamers_api.Helpers;

public static class DateHelper
{
    /// <summary>
    /// Converts a string to a datetimeOffset obj
    /// </summary>
    /// <param name="dateStr"></param>
    /// <returns></returns>
    public static DateTimeOffset ConvertStrToDate(string dateStr)
    {
        bool isParsed = DateTimeOffset.TryParse(dateStr, out DateTimeOffset newDate);

        if (isParsed)
        {
            return newDate;
        }

        return DateTimeOffset.MinValue;
    }
}