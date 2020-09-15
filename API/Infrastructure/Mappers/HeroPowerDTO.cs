using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.DTOs;
using DAL.Models;

namespace API.Infrastructure.Mappers
{
    public class HeroPowerMapper:Profile
    {
        public HeroPowerMapper()
        {
            CreateMap<HeroPower, HeroPowerDTO>()
                .ForMember(dto => dto.PowerId, map => map.MapFrom(s => s.Power.Id))
                .ForMember(dto => dto.Power, map => map.MapFrom(s => s.Power.Name))
                .ForMember(dto => dto.HeroId, map => map.MapFrom(s => s.Hero.Id))
                .ForMember(dto => dto.Hero, map => map.MapFrom(s => s.Hero.Name));
            
            CreateMap<HeroPowerDTO, HeroPower>();
        }
    }
}
