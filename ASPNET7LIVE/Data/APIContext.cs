using ASPNET7LIVE.Models;
using Microsoft.EntityFrameworkCore;
namespace ASPNET7LIVE.Data
{
    public class APIContext: DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
           : base(options)
        {
        }
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
    }
}
