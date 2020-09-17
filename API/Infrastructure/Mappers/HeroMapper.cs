using AutoMapper;
using BusinessLogic.DTOs;
using DAL.Models;
using Microsoft.CodeAnalysis.Operations;
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
            CreateMap<Hero, HeroDTO>();
            CreateMap<HeroDTO, Hero>();

            CreateMap<Hero, HeroCityPowersDTO>()
                .ForMember(dto => dto.CityDTO, map => map.MapFrom(s => s.City))
                .ForMember(dto => dto.HeroPowerDTOs, map => map.MapFrom(s => s.HeroPowers))
                .ForMember(dto => dto.PowersCount, map => map.MapFrom(s => s.HeroPowers.Count()));


            CreateMap<HeroCityPowersDTO, Hero>()
                .ForMember(e => e.CityId, map => map.MapFrom(dto => dto.CityDTO.Id));

        }

    }
}
