using backlog_gamers_api.Models.Enums;

namespace backlog_gamers_api.Config;

/// <summary>
/// Class getting and handling Environment settings
/// </summary>
public class EnvironmentSettings
{
    public EnvironmentSettings()
    {
    }
    
    /// <summary>
    /// The current environment we are in
    /// </summary>
    public EnvironmentType EnvironmentType { get; set; }
    
    /// <summary>
    /// The connection string for our database
    /// </summary>
    public string DbConnStr { get; set; }
    
    /// <summary>
    /// The name of our database
    /// </summary>
    public string DbName { get; set; }

    /// <summary>
    /// Static property for local settings
    /// </summary>
    public static EnvironmentSettings Local =>
        new EnvironmentSettings()
        {
            EnvironmentType = EnvironmentType.Local,
            DbConnStr = Environment.GetEnvironmentVariable("dbConnStr") ?? "",
            DbName = Environment.GetEnvironmentVariable("dbName") ?? "",
        };
    
    /// <summary>
    /// Static property for Development settings
    /// </summary>
    public static EnvironmentSettings Dev =>
        new EnvironmentSettings()
        {
            EnvironmentType = EnvironmentType.Dev,
            DbConnStr = Environment.GetEnvironmentVariable("dbConnStr") ?? "",
            DbName = Environment.GetEnvironmentVariable("dbName") ?? "",
        };

    /// <summary>
    /// Static property for production settings
    /// </summary>
    public static EnvironmentSettings Prod =>
        new EnvironmentSettings()
        {
            //TODO setup amazon secrets manager
            EnvironmentType = EnvironmentType.Prod,
            DbConnStr = Environment.GetEnvironmentVariable("dbConnStr") ?? "",
            DbName = Environment.GetEnvironmentVariable("dbName") ?? "",
        };

    
    /// <summary>
    /// Gets the settings for our current environment
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static EnvironmentSettings GetCurrentEnvSettings()
    {
        EnvironmentSettings envSettings;
        var currentEnv = Environment.GetEnvironmentVariable("currentEnv");

        Enum.TryParse(currentEnv, out EnvironmentType currentEnvType);
        
        return currentEnvType switch
        {
            EnvironmentType.Local => Local,
            EnvironmentType.Dev => Dev,
            EnvironmentType.Prod => Prod,
            _ => Local
        };
    }
}