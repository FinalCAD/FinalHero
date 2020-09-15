using AutoMapper;
using BusinessLogic.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.Mappers
{
    public class HeroMapper : Profile
    {
        public HeroMapper()
        {
            CreateMap<Hero, HeroDTO>()
                .ForMember(dto => dto.CityName, map => map.MapFrom(s => s.City.Name))
                .ForMember(dto => dto.CityId, map => map.MapFrom(s => s.City.Id));


            CreateMap<HeroDTO, Hero>()
                .ForMember(m => m.CityId, map => map.MapFrom(dto => dto.CityId));

        }

    }
}
