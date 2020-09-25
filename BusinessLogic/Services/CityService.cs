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
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CityService : BaseService<City>, ICityService
    {
        private readonly ICityRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        public CityService(ICityRepository repository) : base(repository)
        {
            _repository = repository;
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

        /// <summary>
        /// This service creates a city
        /// </summary>
        /// <param name="name">City's name</param>
        public async Task<CityDTO> Create(string name)
        {
            var check = await GetByNameAsync(name);
            if (check != null)
            {
                throw new BadRequestException("Cannot create City because it already exists");
            }
            var city = new City
            {
                Name = name
            };
            await CreateBase(city);
            return await GetByNameAsync(name);
        }

        /// <summary>
        /// This services updates a city
        /// </summary>
        /// <param name="id">City's id</param>
        /// <param name="name">City's new name</param>
        public async Task<CityDTO> Update(int id, string? name)
        {
            var check = await GetByIdAsync(id);
            if (check == null)
            {
                throw new NotFoundException("Cannot update nonexistant City");
            }
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
        public async Task DeleteById(int id)
        {
            var entity = await _repository.GetCityWithHeroesAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Cannot delete City with id " + id + " because not found");
            }
            if(entity.Heroes.Count > 0)
            {
                foreach (Hero hero in entity.Heroes)
                {
                    hero.CityId = null;
                }
            }
            await _repository.DeleteAsync(entity);
        }

        /// <summary>
        /// This service deletes an city by its name
        /// </summary>
        /// <param name="name">City's name</param>
        public async Task DeleteByName(string name)
        {
            var entity = await GetByNameAsync(name);
            if (entity == null)
            {
                throw new NotFoundException("Cannot delete City with name " + name + " because not found");
            }
            await DeleteById(entity.Id);
        }

        #endregion
    }
}
