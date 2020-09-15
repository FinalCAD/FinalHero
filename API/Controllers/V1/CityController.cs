using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Responses;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]

    public class  CityController : ControllerBase
    {
        #region objects
        private readonly ICityService _cityService;
        #endregion

        #region Constructors
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        #endregion

        #region use cases
        //uses cases:
        //Get All cities.
        /// <summary>
        ///
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// 
        
        [HttpGet]
        [ProducesResponseType(typeof(CityResponseDTO), 200)]
        
        public async Task<CityResponseDTO> GetCities(int offset = 0, int max = 100)
        {
            return await _cityService.GetCities(null,offset,max);
        }


        //Get Top 10 City with more heroes.


        //Get a city by it id with it top 10 heros
        //Get a city with all it heroes
        //Add a city

        //Update a city

        //Delete a city

        #endregion
    }
}
