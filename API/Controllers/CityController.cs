using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.Interfaces;
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
            var response = await _service.GetByIdAsync(id);
            if (response == null)
            {
                throw new NotFoundException("City with id " + id + " not found");
            }
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
            if (response == null)
            {
                throw new NotFoundException("City with name "+ name + " not found");
            }
            return response;
        }

        /// <summary>
        /// This endpoint creates a city
        /// </summary>
        /// <param name="cityDTO">City to create</param>
        [HttpPost]
        public async Task<CityDTO> Post([Required][FromBody] CityDTO cityDTO)
        {
            return await _service.Create(cityDTO.Name);
        }

        /// <summary>
        /// This endpoint updates a city
        /// </summary>
        /// <param name="cityDTO">City to update</param>
        [HttpPut("{city_id}")]
        public async Task<CityDTO> Put([Required] int city_id, [Required][FromBody] CityDTO cityDTO)
        {
            return await _service.Update(city_id, cityDTO.Name);
        }

        /// <summary>
        /// This endpoint deletes a city by its id
        /// </summary>
        /// <param name="city_id">City's id</param>
        /// <returns>Returns nothing if successful 5204)</returns>
        [HttpDelete("id/{city_id}")]
        public async Task<CityDTO> DeleteById([Required] int city_id)
        {
            return await _service.DeleteById(city_id);
        }

        /// <summary>
        /// This endpoint deletes a city by its name
        /// </summary>
        /// <param name="city_name">City's name</param>
        /// <returns>Returns nothing if successful (204)</returns>
        [HttpDelete("name/{city_name}")]
        public async Task<CityDTO> DeleteByName([Required] string city_name)
        {
            return await _service.DeleteByName(city_name);
        }

        #endregion
    }
}
