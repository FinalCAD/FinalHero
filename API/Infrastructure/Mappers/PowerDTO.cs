using AutoMapper;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.Mappers
{
    public class PowerDTO : Profile
    {
        public PowerDTO()
        {
            CreateMap<Power, PowerDTO>();
            CreateMap<PowerDTO, Power>();
        }
    }
}
