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
        private readonly IMapper _mapper;

        public HeroService(
            IHeroRepository repository, 
            ICityService cityService,
            IMapper mapper
            ) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
        }


        #region endpoints

        /// <summary>
        /// Get heroes
        /// </summary>
        /// <param name="offset">Shift of the selections</param>
        /// <param name="max">Result length</param>
        /// <returns>list heros including theirs cities and powers</returns>
        public async Task<HeroResponseDTO> GetHerosWithCityAndPowers(int offset, int max)
        {

            var heroes = await _repository.GetHeroInclCityAndHeroPowersThenPower(offset,max);

            var heroCityPowersDTOs = _mapper.Map<List<HeroCityPowersDTO>>(heroes);


            var total_count = heroCityPowersDTOs.Count();


            return new HeroResponseDTO()
            {
                Entities = heroCityPowersDTOs,
                Meta = new ListMetaData()
                {
                    TotalCount = total_count,
                    Count = offset + Math.Min(max, total_count - offset)
                }
            };
        }


  



        #endregion
    }
}
