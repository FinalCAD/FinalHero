using BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IHeroService
    {
        Task<HeroPowersResponseDTO> GetHerosWithCityAndPowers(int offset, int max);
        //Task AddPowerToHeroAsync(int hero_id, PowerDTO powerDTO);

        //Task<HeroResponseDTO>
    }
}
