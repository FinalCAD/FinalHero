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

            CreateMap<HeroPower, HeroPowerDTO>();

            CreateMap<HeroPowerDTO, HeroPower>();
                
        }
    }
}
