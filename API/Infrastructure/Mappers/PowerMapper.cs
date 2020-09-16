using AutoMapper;
using BusinessLogic.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.Mappers
{
    public class PowerMapper : Profile
    {
        public PowerMapper()
        {
            CreateMap<Power, PowerDTO>();
            CreateMap<PowerDTO, Power>();

            //CreateMap<Power, PowerOrphanDTO>();
            //CreateMap<PowerOrphelinDTO, Power>();


        }
    }
}
