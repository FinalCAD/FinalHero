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
        /// This service deletes a power by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Power> DeleteById(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
            {
                throw new NotFoundException("Cannot delete Power with id " + id + " because not found");
            }
            var heroes = await _heroPowerService.GetAllHeroPowerByPowerAsync(id);
            if (!(heroes is null))
            {
                foreach (HeroPowerDTO heropower in heroes.Entities)
                {
                    await _heroPowerService.DeleteById(heropower.Id);
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
            var entity = Mapper.Map<Power>(await GetByNameAsync(name));
            return Mapper.Map<PowerDTO>(await DeleteById(entity.Id));
        }

        #endregion
    }
}
