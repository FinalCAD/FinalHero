using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IHeroPowerService : IBaseService<HeroPower>
    {
        //Task<List<HeroPower>> GetHeroPowersByHeroId(int id);

        Task DeleteHeroPowersByHeroId(int id);

    }
}
