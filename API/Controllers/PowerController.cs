using System.ComponentModel.DataAnnotations;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using System.Threading.Tasks;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BusinessLogic.Exceptions;

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
        public async Task<PowersResponseDTO> GetAll()
        {
            var response = await _service.GetAllAsync();

            return response;
        }

        /// <summary>
        /// This endpoint gets a power by its id
        /// </summary>
        /// <param name="id">Power's id</param>
        [HttpGet("id/{id}")]
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
        [HttpGet("name/{name}")]
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
        [HttpPost]
        public async Task<PowerDTO> Post([Required][FromBody] PowerDTO powerDTO)
        {
            return await _service.Create(powerDTO);
        }

        /// <summary>
        /// This endpoint updates a power
        /// </summary>
        /// <param name="powerDTO">Power to update</param>
        [HttpPut("{power_id}")]
        public async Task<PowerDTO> Put([Required] int power_id, [Required][FromBody] PowerDTO powerDTO)
        {
            return await _service.Update(power_id, powerDTO.Name, powerDTO.Description);
        }

        /// <summary>
        /// This endpoint deletes a power by its id
        /// </summary>
        /// <param name="power_id">Power's id</param>
        /// <returns>Returns nothing if successful (204)</returns>
        [HttpDelete("id/{power_id}")]
        public async Task<PowerDTO> DeleteById([Required] int power_id)
        {
            return await _service.DeleteById(power_id);
        }

        /// <summary>
        /// This endpoint deletes a power by its name
        /// </summary>
        /// <param name="power_name">Power's name</param>
        /// <returns>Returns nothing if successful (204)</returns>
        [HttpDelete("name/{power_name}")]
        public async Task<PowerDTO> DeleteByName([Required] string power_name)
        {
            return await _service.DeleteByName(power_name);
        }

        #endregion
    }
}
