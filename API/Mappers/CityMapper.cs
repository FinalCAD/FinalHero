using AutoMapper;
using BusinessLogic.DTO;
using DAL.Models;

namespace API.Mappers
{
    /// <summary>
    /// Mapper class for City
    /// </summary>
    public class CityMapper : Profile
    {
        public CityMapper()
        {
            MapCity();
        }

        private void MapCity()
        {
            CreateMap<CityDTO, City>();
            CreateMap<City, CityDTO>();
        }
    }
}


