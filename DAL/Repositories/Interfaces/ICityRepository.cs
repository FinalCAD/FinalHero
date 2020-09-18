using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<City> GetByNameAsync(string name);
    }
}
