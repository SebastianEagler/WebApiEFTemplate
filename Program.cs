
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiEFTemplate.Database;
using WebApiEFTemplate.DatabaseSetup;
using WebApiEFTemplate.Options;

namespace WebApiEFTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

            builder.Services.AddSingleton<DatabaseContextOptionsBuilder>();
            builder.Services.AddSingleton<IConfigureOptions<DbContextOptionsBuilder>, DatabaseContextOptionsBuilder>();

            builder.Services.AddDbContext<DatabaseContext>((serviceProvider, dbContextOptions) =>
            {
             var builder = serviceProvider.GetRequiredService<DatabaseContextOptionsBuilder>();
                builder.Configure(dbContextOptions);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(Options => { Options.EnableTryItOutByDefault(); });
            }           

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
