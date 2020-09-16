using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure.Mappers;
using BusinessLogic.DTOs;
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
        private readonly IHeroService _service;
        #endregion

        #region Constructor
        public HeroController(IHeroService heroService)
        {
            _service = heroService;
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
        [ProducesResponseType(typeof(HeroResponseDTO), 200)]
        public async Task<HeroResponseDTO> GetHerosWithCityAndPowers(int offset = 0, int max = 100)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("the parameters are not correct");
            }

            var heroPowerReponseDTO = await _service.GetHerosWithCityAndPowers(offset, max);

            return heroPowerReponseDTO;
        }

        //UC: He02
        //[ApiVersion("1.0")]
        //[Produces("application/json")]
        //[Route("api/v{version:apiVersion}/hero/Powers")]

        //[HttpPost]
        //public async Task Create([FromBody] HeroDTO heroDTO, [FromBody]PowerDTO powerDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new Exception("the parameters are not correct");
        //    }

        //    await _service.AddHeroWithPowerAsync(heroDTO, powerDTO);

        //}





        #endregion

    }
}
