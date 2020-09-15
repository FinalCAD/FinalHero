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
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class HeroService : BaseService<Hero, IBaseRepository<Hero>>, IHeroService
    {
        private readonly IBaseRepository<Hero> _heroRepository;
        private readonly IBaseRepository<HeroPower> _heroPowerRepository;
        private readonly IMapper _mapper;

        public HeroService(IBaseRepository<Hero> heroRepository, 
            IBaseRepository<HeroPower> heroPowerRepository, 
            IMapper mapper) 
            : base(heroRepository)
        {
            _heroRepository = heroRepository;
            _heroPowerRepository = heroPowerRepository;
            _mapper = mapper;
        }

        
        #region endpoints

        /// <summary>
        /// overload the server coz the fist includable
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public async Task<HeroPowersResponseDTO> GetHerosWithCityAndPowers(int offset, int max)
        {

            var heroPowers = _heroPowerRepository
                .Query(x => true)
                .Include(hp => hp.Power)
                .Include(hp => hp.Hero)
                .ThenInclude(h => h.City)
                .ToList();
       

            var heroPowersDTO = heroPowers.
                GroupBy(hp => hp.Hero.Id)
                .Select(g => new HeroCityPowersDTO
                {
                    Hero = g.FirstOrDefault().Hero.Name,
                    City = g.FirstOrDefault().Hero.City.Name,
                    Powers = g.Select(gg => gg.Power.Name).ToList(),
                    PowersCount = g.Select(gg => gg.Power.Name).Count()
                })
                .OrderBy(hpd => hpd.Hero)
                .Skip(offset)
                .Take(max)
                .ToList();


            var total_count = heroPowersDTO.Count();


            return new HeroPowersResponseDTO()
            {
                Entities = heroPowersDTO.ToList(),
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
            var heroInDb = _heroRepository.GetById(hero_id);
        }

        #endregion
    }
}
