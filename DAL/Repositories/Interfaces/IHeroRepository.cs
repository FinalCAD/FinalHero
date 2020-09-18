using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IHeroRepository : IBaseRepository<Hero>
    {
        Task<Hero> GetHeroWithCityAsync(int id);

        Task<Hero> GetByNameAsync(string name);

        Task<IEnumerable<Hero>> GetAllHeroesByCityAsync(int city_id);
    }
}
