using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Responses;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICityService : IBaseService<City>
    {
        Task<CityResponseDTO> GetCities(Expression<Func<City, bool>> exp = null, int offset = 0, int max = 0);
    }
}
