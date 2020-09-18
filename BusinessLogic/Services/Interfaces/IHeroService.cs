using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IHeroService : IBaseService<Hero>
    {
        Task<HeroesResponseDTO> GetAllAsync();

        Task<HeroesDetailedResponseDTO> GetAllDetailedAsync();

        Task<HeroesResponseDTO> GetAllHeroesByCityAsync(int city_id);

        Task<HeroesPowersResponseDTO> GetAllHeroPowerAsync();

        Task<HeroesPowersResponseDTO> GetAllHeroPowerByHeroAsync(int hero_id);

        Task<HeroesPowersResponseDTO> GetAllHeroPowerByPowerAsync(int power_id);

        Task<HeroPowerDTO> GetHeroPowerByHeroAndPowerAsync(int hero_id, int power_id);

        Task<HeroDetailedDTO> GetByIdDetailedAsync(int id);

        Task<HeroDTO> GetByNameAsync(string name);

        Task<HeroDetailedDTO> GetByNameDetailedAsync(string name);

        Task<HeroPowerDTO> AddHeroPower(HeroPowerDTO dto);

        Task<HeroPowerDTO> UpdateHeroPower(HeroPowerDTO dto);

        Task<HeroDTO> DeleteByName(string name);

        Task<HeroPowerDTO> DeleteHeroPowerById(int id);

        Task<HeroPowerDTO> DeleteHeroPowerByHeroAndPower(int hero_id, int power_id);
    }
}
