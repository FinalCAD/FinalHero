using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        /// UC : He01 : Get heros with theirs cities
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



        /// <summary>
        /// He02 : Create a hero in a city but with powers at least one.
        /// </summary>
        /// <param name="heroDTO">hero with its powers to be added</param>
        /// <returns>the added hero with its power </returns>
        [Route("powers")]
        [HttpPost]
        [ProducesResponseType(typeof(HeroCityPowersDTO), 200)]
        public async Task<HeroCityPowersDTO> CreateHeroWithExistedPowers([FromBody] HeroCityPowersDTO heroDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("the parameters are not correct");
            }

            return await _service.AddNewHeroWithPowersAsync(heroDTO);

        }


        /// <summary>
        /// He03 : Delete hero with its powers.
        /// </summary>
        /// <param name="id">hero id to be delete</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(int),200)]
        public async Task Delete([Required,FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                throw new Exception("Invalid Parameters");
            }

            await _service.DeleteHeroWithPowersAsync(id);
        }
        
        ///In Progress
        /// 
        /// 
        /// <summary>
        /// update an hero with it city and powers
        /// </summary>
        /// <param name="heroCityPowersDTO"></param>
        /// <returns>hero with it city and powers updated</returns>
        [Route("{powers}")]
        [HttpPut]
        [ProducesResponseType(typeof(HeroCityPowersDTO),200)]
        public async Task<HeroCityPowersDTO> Update([Required,FromBody]HeroCityPowersDTO heroCityPowersDTO)
        {
            
            if(!ModelState.IsValid)
            {
                throw new Exception("Invalid parameters");
            }

            await _service.UpdateHeroWithPowers(heroCityPowersDTO);
            return heroCityPowersDTO;
        }
        #endregion

    }
}
