using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure.Mappers;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]

    public class HeroController : ControllerBase
    {
        #region Objects
        private readonly IHeroService _heroServrice;
        #endregion

        #region Constructor
        public HeroController(IHeroService heroService)
        {
            _heroServrice = heroService;
        }
        #endregion


        #region endpoints

        /// <summary>
        /// UC: Get heros with theirs cities
        /// </summary>
        /// <param name="offset">Index to start</param>
        /// <param name="max">Length of of the get list</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(HeroPowersResponseDTO), 200)]
        public async Task<HeroPowersResponseDTO> GetHerosWithCityAndPowers(int offset = 0, int max = 100)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("the parameters are not correct");
            }

            var heroPowerReponseDTO = await _heroServrice.GetHerosWithCityAndPowers(offset, max);

            return heroPowerReponseDTO;
        }


        //[ApiVersion("1.0")]
        //[Produces("application/json")]
        //[Route("api/v{version:apiVersion}/heroes/{hero_id}/Power")]

        //[HttpPost]
        //public async Task Create([Required] int hero_id, [FromBody] PowerDTO powerDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new Exception("the parameters are not correct");
        //    }

        //    await _heroServrice.AddPowerToHeroAsync(hero_id, powerDTO);

        //}





        #endregion

    }
}
