using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Responses;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CityService : BaseService<City, IBaseRepository<City>>, ICityService
    {
        private readonly IBaseRepository<City> _repository;
        private readonly IMapper _mapper;

        public CityService(IBaseRepository<City> repository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CityResponseDTO> GetCities(Expression<Func<City, bool>> exp = null, int offset = 0, int max = 100)
        {
            try
            {
                var entities = await _repository.ListAsync(exp, offset, max);
                var dtos = _mapper.Map<List<CityDTO>>(entities);
                var response = _mapper.Map<CityResponseDTO>(dtos);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
