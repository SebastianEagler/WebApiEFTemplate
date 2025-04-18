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

            var connectString = databaseType == DatabaseType.MsSql ?
                _configuration.GetConnectionString(DatabaseType.MsSql) : _configuration.GetConnectionString(DatabaseType.NpgSql);

            options.DatabaseType = databaseType;
            options.ConnectionString = connectString ?? "";
            _configuration.GetSection("DatabaseOptions").Bind(options);

#if DEBUG
            _logger.LogInformation("ok");
#endif

            //
            // Można dodać inne opcje, które nie są w appsettings.json
            // 

        }
    }
}
