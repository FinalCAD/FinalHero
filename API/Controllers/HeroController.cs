using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Exceptions;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// All operations for heroes
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("heroes")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public HeroController(IHeroService service)
        {
            _service = service;
        }

        #region Methods

        /// <summary>
        /// This endpoint returns all heroes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(HeroesResponseDTO), StatusCodes.Status200OK)]
        public async Task<HeroesResponseDTO> GetAll()
        {
            var response = await _service.GetAllAsync();

            return response;
        }

        /// <summary>
        /// This endpoint returns all heroes with all info
        /// </summary>
        [HttpGet("allinfo")]
        [ProducesResponseType(typeof(HeroesDetailedResponseDTO), StatusCodes.Status200OK)]
        public async Task<HeroesDetailedResponseDTO> GetAllDetailed()
        {
            var response = await _service.GetAllDetailedAsync();

            return response;
        }

        /// <summary>
        /// This endpoint return all heroes from a specific city
        /// </summary>
        /// <param name="city_id">City's id</param>
        /// <response code="404">City with id not found</response>
        [HttpGet("cities/{city_id}")]
        [ProducesResponseType(typeof(HeroesResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroesResponseDTO> GetAllHeroesByCity([Required] int city_id)
        {
            var response = await _service.GetAllHeroesByCityAsync(city_id);

            return response;
        }

        /// <summary>
        /// This endpoint returns all heroes powers
        /// </summary>
        [HttpGet("powers")]
        [ProducesResponseType(typeof(HeroesPowersResponseDTO), StatusCodes.Status200OK)]
        public async Task<HeroesPowersResponseDTO> GetAllHeroPower()
        {
            var response = await _service.GetAllHeroPowerAsync();

            return response;
        }

        /// <summary>
        /// This endpoint gets all heroes powers from a specific hero
        /// </summary>
        /// <param name="hero_id">The hero's id</param>
        /// <response code="404">Hero id not found</response>
        [HttpGet("{hero_id}/powers/")]
        [ProducesResponseType(typeof(HeroesPowersResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByHero([Required] int hero_id)
        {
            var response = await _service.GetAllHeroPowerByHeroAsync(hero_id);

            return response;
        }

        /// <summary>
        /// This endpoint gets all heroes powers from a specific power
        /// </summary>
        /// <param name="power_id">The power's id</param>
        /// <response code="404">Power id not found</response>
        [HttpGet("powers/{power_id}")]
        [ProducesResponseType(typeof(HeroesPowersResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByPower([Required] int power_id)
        {
            var response = await _service.GetAllHeroPowerByPowerAsync(power_id);

            return response;
        }

        /// <summary>
        /// This endpoint gets a hero power from a specific power and hero
        /// </summary>
        /// <response code="404">Hero id or power id not found or hero power not found</response>
        [HttpGet("{hero_id}/powers/{power_id}")]
        [ProducesResponseType(typeof(HeroPowerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroPowerDTO> GetHeroPowerByHeroAndPower([Required] int hero_id, [Required] int power_id)
        {
            var response = await _service.GetHeroPowerByHeroAndPowerAsync(hero_id, power_id);
            
            return response;
        }

        /// <summary>
        /// This endpoint gets a hero by its id
        /// </summary>
        /// <param name="id">Hero's id</param>
        /// <response code="404">Hero id not found</response>
        [HttpGet("id/{id}")]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroDTO> GetById([Required] int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (response == null)
            {
                throw new NotFoundException("Hero with id " + id + " not found");
            }
            return response;
        }

        /// <summary>
        /// This endpoint gets a hero by its id with detailed info
        /// </summary>
        /// <response code="404">Hero id not found</response>
        [HttpGet("id/{id}/allinfo")]
        [ProducesResponseType(typeof(HeroDetailedDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroDetailedDTO> GetByIdDetailed([Required] int id)
        {
            var response = await _service.GetByIdDetailedAsync(id);
            if (response == null)
            {
                throw new NotFoundException("Hero with id " + id + " not found");
            }
            return response;
        }

        /// <summary>
        /// This endpoint gets a hero by its name
        /// </summary>
        /// <response code="404">Hero name not found</response>
        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroDTO> GetByName([Required] string name)
        {
            var response = await _service.GetByNameAsync(name);
            if (response == null)
            {
                throw new NotFoundException("Hero with name " + name + " not found");
            }
            return response;
        }

        /// <summary>
        /// This endpoint gets a hero by its id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">Hero name not found</response>
        [HttpGet("name/{name}/allinfo")]
        [ProducesResponseType(typeof(HeroDetailedDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroDetailedDTO> GetByNameDetailed([Required] string name)
        {
            var response = await _service.GetByNameDetailedAsync(name);
            return response;
        }

        /// <summary>
        /// This endpoint creates a hero
        /// </summary>
        /// <param name="heroDTO">Hero to create</param>
        /// <returns>Created Hero</returns>
        /// <response code="400">Hero with name already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        public async Task<HeroDTO> Post([Required][FromBody] HeroDTO heroDTO)
        {
            return await _service.Create(heroDTO);
        }

        /// <summary>
        /// This endpoint adds a power to a hero
        /// </summary>
        /// <param name="heroPowerDTO">Power to add to hero</param>
        /// <response code="400">Hero power already exists</response>
        /// <response code="404">Hero id or power id not found</response>
        [HttpPost("{hero_id}/powers")]
        [ProducesResponseType(typeof(HeroPowerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroPowerDTO> AddHeroPower([Required] int hero_id, [Required][FromBody] HeroPowerDTO heroPowerDTO)
        {
            heroPowerDTO.HeroId = hero_id;
            return await _service.AddHeroPower(heroPowerDTO);
        }

        /// <summary>
        /// This endpoint updates a hero
        /// </summary>
        /// <param name="heroDTO">Hero to update</param>
        /// <response code="404">Hero with id not found</response>
        [HttpPut("{hero_id}")]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroDTO> Put([Required]int hero_id,[Required][FromBody] HeroDTO heroDTO)
        {
            return await _service.Update(hero_id, heroDTO.Name, heroDTO.CityId);
        }

        /// <summary>
        /// This endpoint updates a power of a hero
        /// </summary>
        /// <param name="heroPowerDTO">Power to update of hero</param>
        /// <returns></returns>
        /// <response code="400">Hero power already exists</response>
        /// <response code="404">Hero id or power id not found or hero power not found</response>
        [HttpPut("{hero_id}/powers/{power_id}")]
        [ProducesResponseType(typeof(HeroPowerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroPowerDTO> UpdateHeroPower([Required] int hero_id, [Required] int power_id, [Required][FromBody] HeroPowerDTO heroPowerDTO)
        {
            return await _service.UpdateHeroPower(hero_id, power_id, heroPowerDTO);
        }


        /// <summary>
        /// This endpoint deletes a hero by its id
        /// </summary>
        /// <param name="hero_id">Hero's id</param>
        /// <returns>Returns nothing if successful</returns>
        /// <response code="404">Hero id not found</response>
        [HttpDelete("id/{hero_id}")]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroDTO> DeleteById([Required] int hero_id)
        {
            return await _service.DeleteById(hero_id);
        }

        /// <summary>
        /// This endpoint deletes a hero by its name
        /// </summary>
        /// <param name="hero_name">Hero's name</param>
        /// <returns>Returns nothing if successful</returns>
        /// <response code="404">Hero name not found</response>
        [HttpDelete("name/{hero_name}")]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroDTO> DeleteByName([Required] string hero_name)
        {
            return await _service.DeleteByName(hero_name);
        }

        /// <summary>
        /// This endpoint deletes a hero power by its id
        /// </summary>
        /// <response code="404">Hero power id not found</response>
        [HttpDelete("powers/{hero_power_id}")]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroPowerDTO> DeleteHeroPowerById([Required] int hero_power_id)
        {
            return await _service.DeleteHeroPowerById(hero_power_id);
        }

        /// <summary>
        /// This endpoint deletes a hero power by its id
        /// </summary>
        /// <response code="404">Hero power not found</response>
        [HttpDelete("{hero_id}/powers/{power_id}")]
        [ProducesResponseType(typeof(HeroDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<HeroPowerDTO> DeleteHeroPowerByHeroAndPower([Required] int hero_id, [Required] int power_id)
        {
            return await _service.DeleteHeroPowerByHeroAndPower(hero_id, power_id);
        }

        #endregion
    }
}
