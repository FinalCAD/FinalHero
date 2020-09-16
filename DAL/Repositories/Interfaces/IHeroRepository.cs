using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IHeroRepository : IBaseRepository<Hero>
    {
        Task<List<Hero>> GetHeroInclCityAndHeroPowersThenPower(int offset,int limit);
        
    }
}
