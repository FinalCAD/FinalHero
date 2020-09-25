using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Context
{
    public class AppContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<HeroPower> HeroesPowers { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
    }
}
