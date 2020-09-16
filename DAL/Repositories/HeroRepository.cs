using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HeroRepository : BaseRepository<Context.AppContext, Hero>, IHeroRepository
    {
        #region properties
        private readonly Context.AppContext _context;
        #endregion

        public HeroRepository(Context.AppContext context) : base(context)
        {
            _context = context;
        }
        #region methods

        public async Task<List<Hero>> GetHeroInclCityAndHeroPowersThenPower(int offset,int limit)
        {
            return await _context.Set<Hero>()
                .Include(h=>h.City)
                .Include(h => h.HeroPowers)
                .ThenInclude(hp=>hp.Power)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }




        #endregion
    }
}
