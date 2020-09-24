using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class HeroPowerService : BaseService<HeroPower>, IHeroPowerService
    {
        private readonly IHeroPowerRepository _repository;

        public HeroPowerService(IHeroPowerRepository repository) : base(repository)
        {
            _repository = repository;
        }

        #region Methods

        /// <summary>
        /// This service gets all heroes powers
        /// </summary>
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerAsync()
        {
            var heroespowers = await _repository.GetAllAsync();
            return new HeroesPowersResponseDTO() { Entities = Mapper.Map<IEnumerable<HeroPowerDTO>>(heroespowers).ToList() };
        }

        /// <summary>
        /// This service gets all heroes powers by hero
        /// </summary>
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByHeroAsync(int hero_id)
        {
            var heroespowers = await _repository.GetAllByHeroIdAsync(hero_id);
            return new HeroesPowersResponseDTO() { Entities = Mapper.Map<IEnumerable<HeroPowerDTO>>(heroespowers).ToList() };
        }

        /// <summary>
        /// This service gets all heroes powers by power
        /// </summary>
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByPowerAsync(int power_id)
        {
            var heroespowers = await _repository.GetAllByPowerIdAsync(power_id);
            return new HeroesPowersResponseDTO() { Entities = Mapper.Map<IEnumerable<HeroPowerDTO>>(heroespowers).ToList() };
        }

        public async Task<IEnumerable<HeroPower>> GetAllByHeroIdWithPowerAsync(int id)
        {
            var heroespowers = await _repository.GetAllByHeroIdWithPowerAsync(id);
            if (heroespowers == null)
            {
                throw new NotFoundException("Hero power with id " + id + " not found");
            }
            return heroespowers;
        }

        /// <summary>
        /// This service gets a hero power by its hero and power
        /// </summary>
        public async Task<HeroPowerDTO> GetHeroPowerByHeroAndPowerAsync(int hero_id, int power_id)
        {
            var heropower = await _repository.GetHeroPowerByHeroIdAndPowerIdAsync(hero_id, power_id);
            if (heropower == null)
            {
                throw new NotFoundException("Hero power with id " + hero_id + " and " + power_id + " not found");
            }
            return Mapper.Map<HeroPowerDTO>(heropower);
        }

        /// <summary>
        /// This service deletes a hero power by its hero and power id
        /// </summary>
        public async Task DeleteHeroPowerByHeroAndPower(int hero_id, int power_id)
        {
            var entity = await _repository.GetHeroPowerByHeroIdAndPowerIdAsync(hero_id, power_id);
            if(entity == null)
            {
                throw new NotFoundException("Cannot delete beacause Hero " + hero_id + " doesn't have power " + power_id);
            }
            await _repository.DeleteAsync(entity);
        }
        #endregion
    }
}
