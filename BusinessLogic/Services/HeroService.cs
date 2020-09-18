using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Responses;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class HeroService : BaseService<Hero, IBaseRepository<Hero>>, IHeroService
    {
        private readonly IHeroRepository _repository;
        private readonly IHeroPowerService _heroPowerService;
        private readonly IPowerService _powerService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public HeroService(
            IHeroRepository repository,
            ICityService cityService,
            IHeroPowerService heroPowerService,
            IPowerService powerService,
            IMapper mapper
            ) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
            _heroPowerService = heroPowerService;
            _cityService = cityService;
            _powerService = powerService;
        }



        #region endpoints

        /// <summary>
        /// Get heroes includding theirs cities and theirs powers
        /// </summary>
        /// <param name="offset">Shift of the selections</param>
        /// <param name="max">Result length</param>
        /// <returns>list heros including theirs cities and powers</returns>
        public async Task<HeroResponseDTO> GetHerosWithCityAndPowers(int offset, int max)
        {

            var heroes = await _repository.GetHeroInclCityAndHeroPowersThenPower(offset, max);

            var heroCityPowersDTOs = _mapper.Map<List<HeroCityPowersDTO>>(heroes);

            var total_count = heroCityPowersDTOs.Count();

            return new HeroResponseDTO()
            {
                Entities = heroCityPowersDTOs,
                Meta = new ListMetaData
                {
                    TotalCount = total_count,
                    Count = offset + Math.Min(max, total_count - offset)
                }
            };
        }


        /// <summary>
        /// Create a new hero with existed power heros
        /// </summary>
        /// <param name="heroDTO"></param>
        /// <returns>the hero with its powers added</returns>
        public async Task<HeroCityPowersDTO> AddNewHeroWithPowersAsync(HeroCityPowersDTO heroDTO)
        {

            var heroPowerDTOs = heroDTO.HeroPowerDTOs;

            // 01 check if parameters are null
            if (heroDTO == null || heroPowerDTOs == null)
                throw new Exception("parameters must be non null");


            // 02 Check if there any power for the new Hero
            if (heroPowerDTOs.Count < 1)
            {
                throw new Exception("Must contain at least one power");
            }


            // 03 check if the city is existed
            if (!await _cityService.ExistedAsync(heroDTO.CityDTO.Id))
            {
                throw new Exception("City not existed");
            }


            //04 check if all the power are existed
            if (!await _heroPowerService.ExistedRangeByIdsAsync(heroPowerDTOs.Select(x => x.PowerId).ToList()))
            {
                throw new Exception("one or more hero powers are not existed");
            }

            // ** 04 and 5 must work in transaction mode 

            // 04 add or update Hero with HeroService
            if (await _repository.ExistEntityAsync(h => h.Name.ToLower() == heroDTO.Name.ToLower()))
            {
                throw new Exception("hero already exist");
            }

            var heroCreated = await _repository.InsertAsync(_mapper.Map<Hero>(heroDTO));


            // 05 add HeroPower Range with HeroWorkService
            var heroPowerEntities = heroPowerDTOs
                .Select(x => new HeroPower
                {
                    HeroId = heroCreated.Id,
                    PowerId = x.PowerId
                })
                .ToList();

            var heroPowersCreated = await _heroPowerService.AddOrUpdateRangeAsync(heroPowerEntities);

            // 06 undatping return results for refreshing the entity in front.
            heroDTO.Id = heroCreated.Id;
            heroDTO.HeroPowerDTOs = _mapper.Map<List<HeroPowerDTO>>(heroPowersCreated);

            return heroDTO;
        }



        public async Task DeleteHeroWithPowersAsync(int heroId)
        {

            //01 check if the hero is existed
            var heroInDb = await _repository.GetByIdAsync(heroId);
            if (heroInDb == null)
            {
                throw new Exception("Unknown Hero ");
            }

            //2 Delete hero's powers entries
            await _heroPowerService.DeleteHeroPowersByHeroId(heroId);

            //3 Delete Hero entry
            await _repository.DeleteAsync(heroInDb);
        }

        public Task<HeroCityPowersDTO> UpdateHeroWithPowers(HeroCityPowersDTO heroCityPowersDTO)
        {
            throw new NotImplementedException();
            //00 check not null
            //if (heroCityPowersDTO != null)
            //    throw new Exception("Entity is null");

            //01 Check if valid hero


            //02 Check if valid City


            //04 check if there is at least one power in the list.


            //03 Check if valid heropowerCity 


            //04 update Hero 


            //05 update (add,remove) list of hero's power in HeroPower set.


            //06 return the update hero with it dependecies
        }


        #endregion
    }
}
