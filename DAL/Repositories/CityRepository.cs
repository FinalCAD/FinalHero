using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CityRepository : BaseRepository<AppContext, City>, ICityRepository
    {
        private readonly AppContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CityRepository(AppContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a city by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<City> GetByNameAsync(string name)
        {
            return await _context.Cities.FirstOrDefaultAsync(e => e.Name == name);
        }

        /// <summary>
        /// Gets city by id with heroes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<City> GetCityWithHeroesAsync(int id)
        {
            return await _context.Cities.Include(e => e.Heroes).FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
