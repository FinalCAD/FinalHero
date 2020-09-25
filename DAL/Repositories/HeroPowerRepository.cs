using DAL.Models;
using DAL.Context;
using DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class HeroPowerRepository : BaseRepository<AppContext, HeroPower>, IHeroPowerRepository
    {
        private readonly AppContext _context;

        public HeroPowerRepository(AppContext context) : base(context)
        {
            _context = context;
        }

        #region Methods

        /// <summary>
        /// Get a hero power by hero and power
        /// </summary>
        public async Task<HeroPower> GetHeroPowerByHeroIdAndPowerIdAsync(int hero_id, int power_id)
        {
            return await _context.HeroesPowers.FirstOrDefaultAsync(e => e.HeroId == hero_id && e.PowerId == power_id);
        }

        public async Task<IEnumerable<HeroPower>> GetAllByHeroIdWithPowerAsync(int id)
        {
            return await _context.HeroesPowers.Include(e => e.Power).Where(e => e.HeroId == id).ToListAsync();
        }

        /// <summary>
        /// Get all heroes power of a hero
        /// </summary>
        public async Task<IEnumerable<HeroPower>> GetAllByHeroIdAsync(int hero_id)
        {
            return await _context.HeroesPowers.Where(e => e.HeroId == hero_id).ToListAsync();
        }

        /// <summary>
        /// Get all heroes power of a power
        /// </summary>
        public async Task<IEnumerable<HeroPower>> GetAllByPowerIdAsync(int power_id)
        {
            return await _context.HeroesPowers.Where(e => e.PowerId == power_id).ToListAsync();
        }

        #endregion
    }
}
