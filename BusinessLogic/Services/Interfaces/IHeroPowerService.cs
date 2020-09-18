using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IHeroPowerService : IBaseService<HeroPower>
    {
        Task<HeroesPowersResponseDTO> GetAllHeroPowerAsync();

        Task<HeroesPowersResponseDTO> GetAllHeroPowerByHeroAsync(int hero_id);

        Task<HeroesPowersResponseDTO> GetAllHeroPowerByPowerAsync(int power_id);

        Task<IEnumerable<HeroPower>> GetAllByHeroIdWithPowerAsync(int id);

        Task<HeroPowerDTO> GetHeroPowerByHeroAndPowerAsync(int hero_id, int power_id);

        Task<HeroPowerDTO> DeleteHeroPowerByHeroAndPower(int hero_id, int power_id);
    }
}
