using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

    }
}
