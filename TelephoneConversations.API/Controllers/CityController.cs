using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TelephoneConversations.API.DTOs;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _dbCity;
        private readonly IMapper _mapper;

        public CityController(ICityRepository dbCity, IMapper mapper)
        {
            _dbCity = dbCity;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCitys()
        {
            IEnumerable<City> cityList = await _dbCity.GetAllAsync();
            return Ok(_mapper.Map<List<CityDTO>>(cityList));
        }

        [HttpGet("{id:int}", Name = "GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var city = await _dbCity.GetAsync(u => u.CityID == id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CityDTO>(city));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CityDTO>> SearchCities(string cityName)
        {
            var cities = await _dbCity.SearchCitiesAsync(cityName);
            return Ok(_mapper.Map<List<CityDTO>>(cities));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityDTO>> CreateCity([FromBody] CityCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }

            if (await _dbCity.GetAsync(u => u.CityName.ToLower() == createDTO.CityName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "City already Exists!");
                return BadRequest();
            }

            City city = _mapper.Map<City>(createDTO);
            await _dbCity.CreateAsync(city);

            return CreatedAtRoute("GetCity", new { id = city.CityID }, city);
        }

        [HttpPut("{id:int}", Name = "UpdateCity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.CityID)
            {
                return BadRequest();
            }

            City model = _mapper.Map<City>(updateDTO);
            await _dbCity.UpdateAsync(model);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteCity")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var city = await _dbCity.GetAsync(u => u.CityID == id);
            if (city == null)
            {
                return NotFound();
            }
            await _dbCity.RemoveAsync(city);
            return NoContent();
        }
    }
}
