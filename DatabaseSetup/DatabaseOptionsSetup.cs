using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Options;


namespace WebApiEFTemplate.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private readonly ILogger<DatabaseOptionsSetup> _logger;
        private readonly IConfiguration _configuration;

        public DatabaseOptionsSetup(ILogger<DatabaseOptionsSetup> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void Configure(DatabaseOptions options)
        {

            var databaseType = _configuration.GetValue<string>("DatabaseType") ?? DatabaseType.NpgSql;

            var connectString = (databaseType == DatabaseType.MsSql ?
                _configuration.GetConnectionString(DatabaseType.MsSql) : _configuration.GetConnectionString(DatabaseType.NpgSql)) ?? "";

            options.ConnectionString = connectString;
            options.DatabaseType = databaseType;
            _configuration.GetSection("DatabaseOptions").Bind(options);

#if DEBUG
            if (connectString.Contains("Server") && connectString.Contains("Database") && connectString.Contains("User Id") && connectString.Contains("Password"))
            {
                _logger.LogInformation("Connection string is ok.");
            }
            else if (string.IsNullOrEmpty(connectString))
            {
                _logger.LogWarning("Connection string is empty. Check appsettings.json");
            }
#endif
            //
            // Można dodać wczytywanie innych opcji, które nie są umiesczone w appsettings.json
            // Albo zrezygnować z appsettings.json i wczytać parametry z innego źródła.
            // 

        }
    }
}
