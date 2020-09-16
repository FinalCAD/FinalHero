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
            CreateMap<Hero, HeroDTO>()
                .ForMember(dto => dto.CityName, map => map.MapFrom(s => s.City.Name))
                .ForMember(dto => dto.CityId, map => map.MapFrom(s => s.City.Id))
                .ForMember(dto => dto.Powers, map => map.MapFrom(s => s.HeroPowers.Select(hp => hp.Power).ToList()))
                .ForMember(dto => dto.PowersCount, map => map.MapFrom(s => s.HeroPowers.Count()))
                ;


            CreateMap<HeroDTO, Hero>()
                .ForMember(m => m.CityId, map => map.MapFrom(dto => dto.CityId));

        }

    }
}
