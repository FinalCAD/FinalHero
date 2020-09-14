using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Responses;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.Mappers
{
    public class CityMapper : Profile
    {
        public CityMapper()
        {
            CreateMap<City, CityDTO>()
               .ForMember(dto => dto.Heroes, map => map.MapFrom(s => s.Heroes == null ? null : s.Heroes));

            CreateMap<List<CityDTO>, CityResponseDTO>()
                .ForPath(r => r.Entities, map => map.MapFrom(s => s));
            
        }
    }
}
