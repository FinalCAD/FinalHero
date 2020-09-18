using AutoMapper;
using BusinessLogic.DTO;
using DAL.Models;

namespace API.Mappers
{
    /// <summary>
    /// Mapper class for Hero
    /// </summary>
    public class HeroMapper : Profile
    {
        public HeroMapper()
        {
            MapHero();
            MapHeroPower();
        }

        private void MapHero()
        {
            CreateMap<HeroDTO, Hero>();
            CreateMap<Hero, HeroDTO>();
        }

        private void MapHeroPower()
        {
            CreateMap<HeroPowerDTO, HeroPower>();
            CreateMap<HeroPower, HeroPowerDTO>();
        }
    }
}
