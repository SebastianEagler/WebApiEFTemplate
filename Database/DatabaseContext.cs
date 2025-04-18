using Microsoft.EntityFrameworkCore;

namespace WebApiEFTemplate.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }        
    }
}
