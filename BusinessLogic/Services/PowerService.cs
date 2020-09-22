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
    public class PowerService : BaseService<Power>, IPowerService
    {
        private readonly IPowerRepository _repository;
        private readonly IHeroPowerService _heroPowerService;

        /// <summary>
        /// Constructor
        /// </summary>
        public PowerService(IPowerRepository repository, IHeroPowerService heroPowerService) : base(repository)
        {
            _repository = repository;
            _heroPowerService = heroPowerService;
        }

        #region Methods

        /// <summary>
        /// This service gets a power by its id
        /// </summary>
        public async Task<PowerDTO> GetByIdAsync(int id)
        {
            return Mapper.Map<PowerDTO>(await _repository.GetByIdAsync(id));
        }

        /// <summary>
        /// This service gets all powers
        /// </summary>
        public async Task<PowersResponseDTO> GetAllAsync()
        {
            var powers = await _repository.GetAllAsync();

            return new PowersResponseDTO() { Entities = Mapper.Map<IEnumerable<PowerDTO>>(powers).ToList() };
        }

        /// <summary>
        /// This service gets an power by its name
        /// </summary>
        /// <param name="name">Power's name</param>
        public async Task<PowerDTO> GetByNameAsync(string name)
        {
            return Mapper.Map<PowerDTO>(await _repository.GetByNameAsync(name));
        }

        /// <summary>
        /// This service creates a power
        /// </summary>
        public async Task<PowerDTO> Create(PowerDTO powerDTO)
        {
            var check = await GetByNameAsync(powerDTO.Name);
            if (check != null)
            {
                throw new BadRequestException("Cannot create Power with name " + powerDTO.Name + " because it already exists");
            }
            var power = new Power
            {
                Name = powerDTO.Name,
                Description = powerDTO.Description
            };
            await CreateBase(power);
            return await GetByNameAsync(power.Name);
        }

        /// <summary>
        /// This service updates a power
        /// </summary>
        public async Task<PowerDTO> Update(int id, string name, string? description)
        {
            var check = await GetByIdAsync(id);
            if (check == null)
            {
                throw new NotFoundException("Cannot update nonexistant Power");
            }
            var power = new Power
            {
                Id = id,
                Name = name,
                Description = description
            };

            await _repository.UpdateAsync(power);
            return await GetByIdAsync(id);
        }

        /// <summary>
        /// This service deletes a power by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PowerDTO> DeleteById(int id)
        {
            var entity = await GetByIdAsyncBase(id);
            if (entity == null)
            {
                throw new NotFoundException("Cannot delete Power with id " + id + " because not found");
            }
            var heroes = await _heroPowerService.GetAllHeroPowerByPowerAsync(id);
            if (heroes != null)
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
        /// This service deletes an power by its name
        /// </summary>
        /// <param name="name">Power's name</param>
        public async Task<PowerDTO> DeleteByName(string name)
        {
            var entity = await GetByNameAsync(name);
            if (entity == null)
            {
                throw new NotFoundException("Cannot delete Power with name " + name + " because not found");
            }
            return Mapper.Map<PowerDTO>(await DeleteByIdBase(entity.Id));
        }

        #endregion
    }
}
