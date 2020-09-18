using DAL.Models;
using System.Threading.Tasks;
using BusinessLogic.DTO.Responses;
using BusinessLogic.DTO;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICityService : IBaseService<City>
    {
        Task<CityDTO> GetByIdAsync(int id);

        Task<CitiesResponseDTO> GetAllAsync();

        Task<CityDTO> GetByNameAsync(string name);

        Task<CityDTO> Create(string name);

        Task<CityDTO> Update(int id, string? name);

        Task<CityDTO> DeleteById(int id);

        Task<CityDTO> DeleteByName(string name);
    }
}
