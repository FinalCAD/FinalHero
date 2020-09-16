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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class HeroService : BaseService<Hero, IBaseRepository<Hero>>, IHeroService
    {
        private readonly IHeroRepository _repository;
        //private readonly IHeroPowerService _heroPowerService;
        private readonly IMapper _mapper;

        //private readonly ICityService _cityService;

        public HeroService(
            IHeroRepository repository, 
            ICityService cityService,
            IMapper mapper
            ) : base(repository)
        {
            _repository = repository;
            //_heroPowerService = heroPowerService;
            _mapper = mapper;
        }


        #region endpoints

        public async Task<HeroResponseDTO> GetHerosWithCityAndPowers(int offset, int max)
        {

            var heroes = await _repository.GetHeroInclCityAndHeroPowersThenPower(offset,max);

            var heroDTOs = _mapper.Map<List<HeroDTO>>(heroes);


            var total_count = heroDTOs.Count();


            return new HeroResponseDTO()
            {
                Entities = _mapper.Map<List<HeroDTO>>(heroDTOs),
                Meta = new ListMetaData()
                {
                    TotalCount = total_count,
                    Count = offset + Math.Min(max, total_count - offset)
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hero_id"></param>
        /// <param name="powerDTO"></param>
        /// <returns></returns>
        public async Task AddPowerToHeroAsync(int hero_id, PowerDTO powerDTO)
        {
            var heroInDb = await _repository.GetById(hero_id);
        }

      

        #endregion
    }
}
