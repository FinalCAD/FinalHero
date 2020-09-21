using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using BusinessLogic.Services.Interfaces;
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
        public async Task<HeroesResponseDTO> GetAll()
        {
            var response = await _service.GetAllAsync();

            return response;
        }

        /// <summary>
        /// This endpoint returns all heroes
        /// </summary>
        [HttpGet("allinfo")]
        public async Task<HeroesDetailedResponseDTO> GetAllDetailed()
        {
            var response = await _service.GetAllDetailedAsync();

            return response;
        }

        /// <summary>
        /// This endpoint return all heroes from a specific city
        /// </summary>
        [HttpGet("cities/{city_id}")]
        public async Task<HeroesResponseDTO> GetAllHeroesByCity([Required] int city_id)
        {
            var response = await _service.GetAllHeroesByCityAsync(city_id);

            return response;
        }

        /// <summary>
        /// This endpoint returns all heroes powers
        /// </summary>
        [HttpGet("powers")]
        public async Task<HeroesPowersResponseDTO> GetAllHeroPower()
        {
            var response = await _service.GetAllHeroPowerAsync();

            return response;
        }

        /// <summary>
        /// This endpoint gets all heroes powers from a specific hero
        /// </summary>
        /// <param name="hero_id">The hero's id</param>
        [HttpGet("{hero_id}/powers/")]
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByHero([Required] int hero_id)
        {
            var response = await _service.GetAllHeroPowerByHeroAsync(hero_id);

            return response;
        }

        /// <summary>
        /// This endpoint gets all heroes powers from a specific power
        /// </summary>
        /// <param name="hero_id">The hero's id</param>
        [HttpGet("powers/{power_id}")]
        public async Task<HeroesPowersResponseDTO> GetAllHeroPowerByPower([Required] int power_id)
        {
            var response = await _service.GetAllHeroPowerByPowerAsync(power_id);

            return response;
        }

        /// <summary>
        /// This endpoint gets a hero power from a specific power and hero
        /// </summary>
        [HttpGet("{hero_id}/powers/{power_id}")]
        public async Task<HeroPowerDTO> GetHeroPowerByHeroAndPower([Required] int hero_id, [Required] int power_id)
        {
            var response = await _service.GetHeroPowerByHeroAndPowerAsync(hero_id, power_id);

            return response;
        }

        /// <summary>
        /// This endpoint gets a hero by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        public async Task<HeroDTO> GetById([Required] int id, bool? detailed)
        {
            return await _service.GetByIdAsync(id);
        }

        /// <summary>
        /// This endpoint gets a hero by its id with detailed info
        /// </summary>
        [HttpGet("id/{id}/allinfo")]
        public async Task<HeroDetailedDTO> GetByIdDetailed([Required] int id)
        {
            return await _service.GetByIdDetailedAsync(id);
        }

        /// <summary>
        /// This endpoint gets a hero by its name
        /// </summary>
        [HttpGet("name/{name}")]
        public async Task<HeroDTO> GetByName([Required] string name)
        {
            return await _service.GetByNameAsync(name);
        }

        /// <summary>
        /// This endpoint gets a hero by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("name/{name}/allinfo")]
        public async Task<HeroDetailedDTO> GetByNameDetailed([Required] string name)
        {
            return await _service.GetByNameDetailedAsync(name);
        }

        /// <summary>
        /// This endpoint creates a hero
        /// </summary>
        /// <param name="heroDTO">Hero to create</param>
        [HttpPost]
        public async Task<HeroDTO> Post([Required][FromBody] HeroDTO heroDTO)
        {
            return await _service.Create(heroDTO);
        }

        /// <summary>
        /// This endpoint adds a power to a hero
        /// </summary>
        /// <param name="heroPowerDTO">Power to add to hero</param>
        /// <returns></returns>
        [HttpPost("{hero_id}/powers")]
        public async Task<HeroPowerDTO> AddHeroPower([Required] int hero_id, [Required][FromBody] HeroPowerDTO heroPowerDTO)
        {
            heroPowerDTO.HeroId = hero_id;
            return await _service.AddHeroPower(heroPowerDTO);
        }

        /// <summary>
        /// This endpoint updates a hero
        /// </summary>
        /// <param name="heroDTO">Hero to update</param>
        [HttpPut("{hero_id}")]
        public async Task<HeroDTO> Put([Required]int hero_id,[Required][FromBody] HeroDTO heroDTO)
        {
            return await _service.Update(hero_id, heroDTO.Name, heroDTO.CityId);
        }

        /// <summary>
        /// This endpoint updates a power of a hero
        /// </summary>
        /// <param name="heroPowerDTO">Power to update of hero</param>
        /// <returns></returns>
        [HttpPut("{hero_id}/powers/{power_id}")]
        public async Task<HeroPowerDTO> UpdateHeroPower([Required] int hero_id, [Required] int power_id, [Required][FromBody] HeroPowerDTO heroPowerDTO)
        {
            return await _service.UpdateHeroPower(hero_id, power_id, heroPowerDTO);
        }


        /// <summary>
        /// This endpoint deletes a hero by its id
        /// </summary>
        /// <param name="hero_id">Hero's id</param>
        /// <returns>Returns nothing if successful</returns>
        [HttpDelete("id/{hero_id}")]
        public async Task<HeroDTO> DeleteById([Required] int hero_id)
        {
            return await _service.DeleteById(hero_id);
        }

        /// <summary>
        /// This endpoint deletes a hero by its name
        /// </summary>
        /// <param name=hero_name">Hero's name</param>
        /// <returns>Returns nothing if successful</returns>
        [HttpDelete("name/{hero_name}")]
        public async Task<HeroDTO> DeleteByName([Required] string hero_name)
        {
            return await _service.DeleteByName(hero_name);
        }

        /// <summary>
        /// This endpoint deletes a hero power by its id
        /// </summary>
        [HttpDelete("powers/{hero_power_id}")]
        public async Task<HeroPowerDTO> DeleteHeroPowerById([Required] int hero_power_id)
        {
            return await _service.DeleteHeroPowerById(hero_power_id);
        }

        /// <summary>
        /// This endpoint deletes a hero power by its id
        /// </summary>
        [HttpDelete("{hero_id}/powers/{power_id}")]
        public async Task<HeroPowerDTO> DeleteHeroPowerByHeroAndPower([Required] int hero_id, [Required] int power_id)
        {
            return await _service.DeleteHeroPowerByHeroAndPower(hero_id, power_id);
        }

        #endregion
    }
}
