using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HeroRepository : BaseRepository<AppContext, Hero>, IHeroRepository
    {
        private readonly AppContext _context;

        public HeroRepository(AppContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets hero by id with city
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Hero> GetHeroWithCityAsync(int id)
        {
            return await _context.Heroes.Include(e => e.City).FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Get a hero by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Hero> GetByNameAsync(string name)
        {
            return await _context.Heroes.FirstOrDefaultAsync(e => e.Name == name);
        }

        /// <summary>
        /// Get all heroes from a specific city
        /// </summary>
        public async Task<IEnumerable<Hero>> GetAllHeroesByCityAsync(int city_id)
        {
            return await _context.Heroes.Where(e => e.CityId == city_id).ToListAsync();
        }
    }
}
