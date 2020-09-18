﻿using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class HeroService : BaseService<Hero>, IHeroService
    {
        private readonly IHeroRepository _repository;
        private readonly IHeroPowerService _heroPowerService;

        /// <summary>
        /// Constructor
        /// </summary>
        public HeroService(IHeroRepository repository, IHeroPowerService heroPowerService) : base(repository)
        {
            _repository = repository;
            _heroPowerService = heroPowerService;
        }

        #region Methods

        /// <summary>
        /// This service gets a hero by its id
        /// </summary>
        public async Task<HeroDTO> GetByIdAsync(int id)
        {
            return Mapper.Map<HeroDTO>(await _repository.GetByIdAsync(id));
        }

        /// <summary>
        /// This service gets all heroes
        /// </summary>
        public async Task<HeroesResponseDTO> GetAllAsync()
        {
            var heroes = await _repository.GetAllAsync();

            return new HeroesResponseDTO() { Entities = Mapper.Map<IEnumerable<HeroDTO>>(heroes).ToList() };
        }

        /// <summary>
        /// This service gets all heroes with all infos
        /// </summary>
        /// <returns></returns>
        public async Task<HeroesDetailedResponseDTO> GetAllDetailedAsync()
        {
            var heroes = Mapper.Map<IEnumerable<HeroDTO>>(await _repository.GetAllAsync()).ToList();
            var heroesDetailed = new List<HeroDetailedDTO>();
            foreach (HeroDTO hero in heroes)
            {
                heroesDetailed.Add(await GetByIdDetailedAsync(hero.Id));
            }

            return new HeroesDetailedResponseDTO() { Entities = heroesDetailed};
        }

        /// <summary>
        /// This service gets all heroes by city
        /// </summary>
        /// <param name="city_id">The city's id</param>
        public async Task<HeroesResponseDTO> GetAllHeroesByCityAsync(int city_id)
        {
            var heroes = await _repository.GetAllHeroesByCityAsync(city_id);

            return new HeroesResponseDTO() { Entities = Mapper.Map<IEnumerable<HeroDTO>>(heroes).ToList() };
        }

        /// <summary>
        /// This service gets all heroes powers
        /// </summary>
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerAsync()
        {
            return await _heroPowerService.GetAllHeroPowerAsync();
        }

        /// <summary>
        /// This service gets all heroes powers by hero
        /// </summary>
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByHeroAsync(int hero_id)
        {
            return await _heroPowerService.GetAllHeroPowerByHeroAsync(hero_id);
        }

        /// <summary>
        /// This service gets all heroes powers by power
        /// </summary>
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByPowerAsync(int power_id)
        {
            return await _heroPowerService.GetAllHeroPowerByPowerAsync(power_id);
            
        }

        /// <summary>
        /// This service gets a hero power by its hero and power
        /// </summary>
        public async Task<HeroPowerDTO> GetHeroPowerByHeroAndPowerAsync(int hero_id, int power_id)
        {
            return await _heroPowerService.GetHeroPowerByHeroAndPowerAsync(hero_id, power_id);
        }

        /// <summary>
        /// This service gets a hero and all things related to him by its id
        /// </summary>
        /// <param name="id">Hero's id</param>
        public async Task<HeroDetailedDTO> GetByIdDetailedAsync(int id)
        {
            var hero = await _repository.GetHeroWithCityAsync(id);
            if(hero == null)
            {
                return null;
            }            
            var heropowers = await _heroPowerService.GetAllByHeroIdWithPowerAsync(id);          
            var powers = new List<PowerDTO>();
            foreach (HeroPower heropower in heropowers.ToList())
            {
                powers.Add(Mapper.Map<PowerDTO>(heropower.Power));
            }
            
            return new HeroDetailedDTO()
            {
                Id = hero.Id,
                Name = hero.Name,
                City = Mapper.Map<CityDTO>(hero.City),
                Powers = powers
            };
        }

        /// <summary>
        /// This service gets a hero by its name
        /// </summary>
        /// <param name="name">Hero's name</param>
        public async Task<HeroDTO> GetByNameAsync(string name)
        {
            return Mapper.Map<HeroDTO>(await _repository.GetByNameAsync(name));
        }

        /// <summary>
        /// This service gets a hero and all things related to him by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<HeroDetailedDTO> GetByNameDetailedAsync(string name)
        {
            var hero = await GetByNameAsync(name);
            if (hero is null)
            {
                return null;
            }
            return await GetByIdDetailedAsync(hero.Id);
        }

        /// <summary>
        /// This service creates a hero
        /// </summary>
        public async Task<HeroDTO> Create(HeroDTO heroDTO)
        {
            var check = await GetByNameAsync(heroDTO.Name);
            if (!(check is null))
            {
                throw new BadRequestException("Cannot create Hero with name "+ heroDTO.Name +" because it already exists");
            }
            var hero = new Hero
            {
                Name = heroDTO.Name,
                CityId = heroDTO.CityId
            };
            await CreateBase(hero);
            return await GetByNameAsync(hero.Name);
        }

        /// <summary>
        /// This service adds a hero power
        /// </summary>
        public async Task<HeroPowerDTO> AddHeroPower(HeroPowerDTO dto)
        {
            return Mapper.Map<HeroPowerDTO>(await _heroPowerService.CreateBase(Mapper.Map<HeroPower>(dto)));
        }

        /// <summary>
        /// This service updates a hero
        /// </summary>
        public async Task<HeroDTO> Update(int id, string name, int? city_id)
        {
            var hero = new Hero
            {
                Id = id,
                Name = name,
                CityId = city_id
            };

            await _repository.UpdateAsync(hero);
            return Mapper.Map<HeroDTO>(await GetByIdAsyncBase(id));
        }

        /// <summary>
        /// This service updates a hero power
        /// </summary>
        public async Task<HeroPowerDTO> UpdateHeroPower(HeroPowerDTO dto)
        {
            return Mapper.Map<HeroPowerDTO>(await _heroPowerService.UpdateBase(Mapper.Map<HeroPower>(dto)));
        }

        /// <summary>
        /// This service deletes a hero by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HeroDTO> DeleteById(int id)
        {
            var entity = await GetByIdAsyncBase(id);
            if (entity is null)
            {
                throw new NotFoundException("Cannot delete Hero with id " + id + " because not found");
            }
            var heroes = await _heroPowerService.GetAllHeroPowerByHeroAsync(id);
            if (!(heroes is null))
            {
                foreach (HeroPowerDTO heropower in heroes.Entities)
                {
                    await _heroPowerService.DeleteByIdBase(heropower.Id);
                }
            }
            await _repository.DeleteAsync(entity);
            return await GetByIdAsync(id);
        }

        /// <summary>
        /// This service deletes an hero by its name
        /// </summary>
        /// <param name="name">Hero's name</param>
        public async Task<HeroDTO> DeleteByName(string name)
        {
            var entity = await GetByNameAsync(name);
            return await DeleteById(entity.Id);
        }

        /// <summary>
        /// This service deletes a hero power by its id
        /// </summary>
        public async Task<HeroPowerDTO> DeleteHeroPowerById(int id)
        {
            return Mapper.Map<HeroPowerDTO>(await _heroPowerService.DeleteByIdBase(id));
        }

        /// <summary>
        /// This service deletes a hero power by its hero and power id
        /// </summary>
        public async Task<HeroPowerDTO> DeleteHeroPowerByHeroAndPower(int hero_id, int power_id)
        {
            return await _heroPowerService.DeleteHeroPowerByHeroAndPower(hero_id, power_id);
        }

        #endregion
    }
}
