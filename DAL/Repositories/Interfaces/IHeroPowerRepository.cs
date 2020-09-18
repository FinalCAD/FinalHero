using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IHeroPowerRepository : IBaseRepository<HeroPower>
    {
        Task<HeroPower> GetHeroPowerByHeroIdAndPowerIdAsync(int hero_id, int power_id);

        Task<IEnumerable<HeroPower>> GetAllByHeroIdWithPowerAsync(int id);

        Task<IEnumerable<HeroPower>> GetAllByHeroIdAsync(int hero_id);
        
        Task<IEnumerable<HeroPower>> GetAllByPowerIdAsync(int power_id);
    }
}
