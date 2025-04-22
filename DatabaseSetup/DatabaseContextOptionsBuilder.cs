using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiEFTemplate.Options;

namespace WebApiEFTemplate.DatabaseSetup
{
    public class DatabaseContextOptionsBuilder : IConfigureOptions<DbContextOptionsBuilder>
    {
        private readonly IOptions<DatabaseOptions> _options;

        public DatabaseContextOptionsBuilder(IOptions<DatabaseOptions> options)
        {
            _options = options;
        }

        public void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            var dbOptions = _options.Value;

            switch (dbOptions.DatabaseType)
            {
                case DatabaseType.NpgSql:
                    optionsBuilder.UseNpgsql(dbOptions.ConnectionString, sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(dbOptions.MaxRetryCount, TimeSpan.FromSeconds(dbOptions.MaxRetryDelay), null);
                        sqlOptions.CommandTimeout(dbOptions.CommandTimeout);
                    });
                    break;
                case DatabaseType.MsSql:                    
                    optionsBuilder.UseSqlServer(dbOptions.ConnectionString, sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(dbOptions.MaxRetryCount, TimeSpan.FromSeconds(dbOptions.MaxRetryDelay), null);
                        sqlOptions.CommandTimeout(dbOptions.CommandTimeout);
                    });
                     break;
                default:
                    throw new NotSupportedException($"Database type '{dbOptions.DatabaseType}' is not supported.");
            }
            optionsBuilder.EnableDetailedErrors(dbOptions.EnableDetailedErrors);// tylko do debugowania
            optionsBuilder.EnableSensitiveDataLogging(dbOptions.EnableSensitiveDataLogging);
                        

        }

    }
}
