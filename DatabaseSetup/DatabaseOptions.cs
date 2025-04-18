namespace WebApiEFTemplate.Options
{
    
    public static class DatabaseType
    {
        public const string MsSql = "MsSql";
        public const string NpgSql = "NpgSql";  
    }

    public class DatabaseOptions
    {        
        public string DatabaseType { get; set; } = string.Empty; // MsSql, NpgSql   
        public string ConnectionString { get; set; } = string.Empty;
        public int CommandTimeout { get; set; } = 60;
        public int MaxRetryCount { get; set; } = 3;
        public int MaxRetryDelay { get; set; } = 3;
        public bool EnableSensitiveDataLogging { get; set; }
        public bool EnableDetailedErrors { get; set; }  
    }

}
