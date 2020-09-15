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
    public class HeroPowerRepository : BaseRepository<Context.AppContext, HeroPower>, IHeroPowerRepository
    {
        #region properties
        private readonly Context.AppContext _context;

        #endregion

        public HeroPowerRepository(Context.AppContext context) : base(context)
        {
            _context = context;
 
        }

        #region methods

        //public Task<List<HeroPower>> ListHerosWithCityAndPowersAsync(Expression<Func<Hero, bool>> exp, int offset, int max)
        //{
        //    //var result =_repository.Query(exp,offset,max)
        //    //    .Include(hp => hp.)
        //    //    .Include(hp => hp.Power)
        //    //    .OrderBy(hp => hp.Hero.name)
        //    //    .Skip(offset)
        //    //    .Take(max)
        //    //    .ToListAsync();
        //    //return result;
        //}

        //}

        #endregion
    }
}
