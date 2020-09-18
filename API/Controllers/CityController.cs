using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{ 
    /// <summary>
    /// All operations of cities
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public CityController(ICityService service)
        {
            _service = service;
        }

        #region Methods

        /// <summary>
        /// This endpoint returns all cities
        /// </summary>
        [HttpGet]
        public async Task<CitiesResponseDTO> GetAll()
        {
            var response = await _service.GetAllAsync();

            return response;
        }

        /// <summary>
        /// This endpoint gets a city by its id
        /// </summary>
        /// <param name="id">City's id</param>
        [HttpGet("id/{id}")]
        public async Task<CityDTO> GetById([Required] int id)
        {
            var cities = await _service.GetByIdAsync(id);

            var response = Mapper.Map<CityDTO>(cities);

            return response;
        }

        /// <summary>
        /// This endpoint gets a city by its name
        /// </summary>
        /// <param name="name">City's name</param>
        [HttpGet("name/{name}")]
        public async Task<CityDTO> GetByName([Required] string name)
        {
            var response = await _service.GetByNameAsync(name);

            return response;
        }

        /// <summary>
        /// This endpoint creates a city
        /// </summary>
        /// <param name="cityDTO">City to create</param>
        [HttpPost]
        public async Task<CityDTO> Post([Required][FromBody] CityDTO cityDTO)
        {
            return Mapper.Map<CityDTO>(await _service.Create(Mapper.Map<City>(cityDTO)));
        }

        /// <summary>
        /// This endpoint updates a city
        /// </summary>
        /// <param name="cityDTO">City to update</param>
        [HttpPut("{city_id}")]
        public async Task<CityDTO> Put([Required][FromBody] CityDTO cityDTO)
        {
            return Mapper.Map<CityDTO>(await _service.Update(Mapper.Map<City>(cityDTO)));
        }

        /// <summary>
        /// This endpoint deletes a city by its id
        /// </summary>
        /// <param name="city_id">City's id</param>
        /// <returns>Returns nothing if successful 5204)</returns>
        [HttpDelete("id/{city_id}")]
        public async Task<CityDTO> DeleteById([Required] int city_id)
        {
            return Mapper.Map<CityDTO>(await _service.DeleteById(city_id));
        }

        /// <summary>
        /// This endpoint deletes a city by its name
        /// </summary>
        /// <param name="city_name">City's name</param>
        /// <returns>Returns nothing if successful (204)</returns>
        [HttpDelete("name/{city_name}")]
        public async Task<CityDTO> DeleteByName([Required] string city_name)
        {
            return Mapper.Map<CityDTO>(await _service.DeleteByName(city_name));
        }

        #endregion
    }
}
