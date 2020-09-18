using DAL.Models;
using System.Threading.Tasks;
using BusinessLogic.DTO.Responses;
using BusinessLogic.DTO;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICityService : IBaseService<City>
    {
        Task<CitiesResponseDTO> GetAllAsync();

        Task<CityDTO> GetByNameAsync(string name);

        Task<CityDTO> DeleteByName(string name);
    }
}
