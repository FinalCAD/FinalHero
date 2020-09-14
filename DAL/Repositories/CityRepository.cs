using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CityRepository : BaseRepository<AppContext, City>, ICityRepository
    {
        #region properties
        private readonly AppContext _context;
        #endregion

        public CityRepository(AppContext context) : base(context)
        {
            _context = context;
        }

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<City> GetCityByIdWithHeroesAsync(int id)
        {
            return await _context.Set<City>().Include(e => e.Heroes).FirstOrDefaultAsync();
        }

        #endregion

    }
}
