using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        /// <returns>List of all cities</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CitiesResponseDTO), StatusCodes.Status200OK)]
        public async Task<CitiesResponseDTO> GetAll()
        {
            var response = await _service.GetAllAsync();

            return response;
        }

        /// <summary>
        /// This endpoint gets a city by its id
        /// </summary>
        /// <param name="id">City's id</param>
        /// <response code="404">NotFound : Unknown id</response>
        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(CityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
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
        /// <reponse code="404">NotFound : Unknown name</reponse>
        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(CityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
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
        /// <reponse code="400">BadRequest : City already exist with the same name</reponse>
        [HttpPost]
        [ProducesResponseType(typeof(CityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        public async Task<CityDTO> Post([Required][FromBody] CityDTO cityDTO)
        {
            return await _service.Create(cityDTO.Name);
        }

        /// <summary>
        /// This endpoint updates a city
        /// </summary>
        /// <param name="city_id">City's id</param>
        /// <param name="cityDTO">City to update</param>
        /// <reponse code="404">NotFound : City with id not found</reponse>
        [HttpPut("{city_id}")]
        [ProducesResponseType(typeof(CityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<CityDTO> Put([Required] int city_id, [Required][FromBody] CityDTO cityDTO)
        {
            return await _service.Update(city_id, cityDTO.Name);
        }

        /// <summary>
        /// This endpoint deletes a city by its id
        /// </summary>
        /// <param name="city_id">City's id</param>
        /// <returns>Returns nothing if successful</returns>
        /// <reponse code="404">NotFound : City with id not found</reponse>
        [ProducesResponseType(typeof(CityDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
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
        /// <reponse code="404">NotFound : City with name not found</reponse>
        [HttpDelete("name/{city_name}")]
        [ProducesResponseType(typeof(CityDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<CityDTO> DeleteByName([Required] string city_name)
        {
            return await _service.DeleteByName(city_name);
        }

        #endregion
    }
}
