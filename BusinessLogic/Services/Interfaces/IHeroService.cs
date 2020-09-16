using BusinessLogic.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IHeroService:IBaseService<Hero>
    {
        Task<HeroResponseDTO> GetHerosWithCityAndPowers(int offset, int max);

    }
}
