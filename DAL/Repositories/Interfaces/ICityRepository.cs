using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ICityRepository: IBaseRepository<City>
    {
        Task<City> GetCityByIdWithHeroesAsync(int id);
    }
}
