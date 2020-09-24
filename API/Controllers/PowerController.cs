using System.ComponentModel.DataAnnotations;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using System.Threading.Tasks;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    /// <summary>
    /// All operations for powers
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("powers")]
    public class PowerController : ControllerBase
    {
        private readonly IPowerService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public PowerController(IPowerService service)
        {
            _service = service;
        }

        #region Methods

        /// <summary>
        /// This endpoints returns all powers
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PowersResponseDTO), StatusCodes.Status200OK)]
        public async Task<PowersResponseDTO> GetAll()
        {
            var response = await _service.GetAllAsync();

            return response;
        }

        /// <summary>
        /// This endpoint gets a power by its id
        /// </summary>
        /// <param name="id">Power's id</param>
        /// <response code="404">Power with id not found</response>
        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(PowerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<PowerDTO> GetById([Required] int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (response == null)
            {
                throw new NotFoundException("Power with id " + id + " not found");
            }
            return response;
        }

        /// <summary>
        /// This endpoint gets a power by its name
        /// </summary>
        /// <param name="name">Power's name</param>
        /// <response code="404">Power with name not found</response>
        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(PowerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<PowerDTO> GetByName([Required] string name)
        {
            var response = await _service.GetByNameAsync(name);
            if (response == null)
            {
                throw new NotFoundException("Power with name " + name + " not found");
            }
            return response;
        }

        /// <summary>
        /// This endpoint creates a power
        /// </summary>
        /// <param name="powerDTO">Power to create</param>
        /// <response code="400">Power with name already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(PowerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        public async Task<PowerDTO> Post([Required][FromBody] PowerDTO powerDTO)
        {
            return await _service.Create(powerDTO);
        }

        /// <summary>
        /// This endpoint updates a power
        /// </summary>
        /// <param name="power_id">Power's id</param>
        /// <param name="powerDTO">Power to update</param>
        /// <response code="404">Power not found</response>
        [HttpPut("{power_id}")]
        [ProducesResponseType(typeof(PowerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<PowerDTO> Put([Required] int power_id, [Required][FromBody] PowerDTO powerDTO)
        {
            return await _service.Update(power_id, powerDTO.Name, powerDTO.Description);
        }

        /// <summary>
        /// This endpoint deletes a power by its id
        /// </summary>
        /// <param name="power_id">Power's id</param>
        /// <returns>Returns nothing if successful</returns>
        /// <response code="404">Power not found</response>
        [HttpDelete("id/{power_id}")]
        [ProducesResponseType(typeof(PowerDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<PowerDTO> DeleteById([Required] int power_id)
        {
            return await _service.DeleteById(power_id);
        }

        /// <summary>
        /// This endpoint deletes a power by its name
        /// </summary>
        /// <param name="power_name">Power's name</param>
        /// <returns>Returns nothing if successful</returns>
        /// <response code="404">Power not found</response>
        [HttpDelete("name/{power_name}")]
        [ProducesResponseType(typeof(PowerDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<PowerDTO> DeleteByName([Required] string power_name)
        {
            return await _service.DeleteByName(power_name);
        }

        #endregion
    }
}
