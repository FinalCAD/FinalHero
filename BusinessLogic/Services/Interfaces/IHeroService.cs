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
        Task<HeroDTO> GetByIdAsync(int id);

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

        Task<HeroDTO> Create(HeroDTO heroDTO);

        Task<HeroPowerDTO> AddHeroPower(HeroPowerDTO dto);

        Task<HeroDTO> Update(int id, string name, int? city_id);

        Task<HeroPowerDTO> UpdateHeroPower(int hero_id, int power_id, HeroPowerDTO dto);

        Task DeleteById(int id);

        Task DeleteByName(string name);

        Task DeleteHeroPowerById(int id);

        Task DeleteHeroPowerByHeroAndPower(int hero_id, int power_id);
    }
}
