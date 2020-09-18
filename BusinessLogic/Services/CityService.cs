using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CityService : BaseService<City>, ICityService
    {
        private readonly ICityRepository _repository;
        private readonly IHeroService _heroService;

        /// <summary>
        /// Constructor
        /// </summary>
        public CityService(ICityRepository repository , IHeroService heroService) : base(repository)
        {
            _repository = repository;
            _heroService = heroService;
        }

        #region Methods

        /// <summary>
        /// This service gets a city by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CityDTO> GetByIdAsync(int id)
        {
            var entity = Mapper.Map<CityDTO>(await GetByIdAsyncBase(id));
            return entity;
        }


        /// <summary>
        /// This service gets all cities
        /// </summary>
        public async Task<CitiesResponseDTO> GetAllAsync()
        {
            var cities = await _repository.GetAllAsync();

            return new CitiesResponseDTO() { Entities = Mapper.Map<IEnumerable<CityDTO>>(cities).ToList() };
        }

        /// <summary>
        /// This service gets an city by its name
        /// </summary>
        /// <param name="name">City's name</param>
        public async Task<CityDTO> GetByNameAsync(string name)
        {
            var city = await _repository.GetByNameAsync(name);
            return Mapper.Map<CityDTO>(city);
        }

        public async Task<CityDTO> Create(string name)
        {
            var city = new City
            {
                Name = name
            };
            await CreateBase(city);
            return await GetByNameAsync(name);
        }

        public async Task<CityDTO> Update(int id, string? name)
        {
            var city = new City
            {
                Id = id,
                Name = name
            };

            await _repository.UpdateAsync(city);
            return await GetByIdAsync(id);
        }

        /// <summary>
        /// This service deletes a city by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CityDTO> DeleteById(int id)
        {
            var entity = await GetByIdAsyncBase(id);
            if (entity is null)
            {
                throw new NotFoundException("Cannot delete City with id " + id + " because not found");
            }
            var heroes = await _heroService.GetAllHeroesByCityAsync(id);
            if(!(heroes is null))
            {
                foreach (HeroDTO hero in heroes.Entities)
                {
                    await _heroService.UpdateBase(new Hero
                    {
                        Id = hero.Id,
                        Name = hero.Name,
                        CityId = null
                    });
                }
            }
            await _repository.DeleteAsync(entity);
            return await GetByIdAsync(id);
        }

        /// <summary>
        /// This service deletes an city by its name
        /// </summary>
        /// <param name="name">City's name</param>
        public async Task<CityDTO> DeleteByName(string name)
        {
            var entity = await GetByNameAsync(name);
            if (entity is null)
            {
                throw new NotFoundException("Cannot delete City with name " + name + " because not found");
            }
            await DeleteById(entity.Id);
            return await GetByNameAsync(name);
            /**await _repository.DeleteAsync(entity);
            return await GetByNameAsync(name);*/
        }

        #endregion
    }
}
