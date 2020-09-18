using AutoMapper;
using BusinessLogic.DTO;
using DAL.Models;

namespace API.Mappers
{
    /// <summary>
    /// Mapper class for Power
    /// </summary>
    public class PowerMapper : Profile
    {
        public PowerMapper()
        {
            MapPower();
        }

        private void MapPower()
        {
            CreateMap<PowerDTO, Power>();
            CreateMap<Power, PowerDTO>();
        }
    }
}
