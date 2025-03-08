using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.DTOs;
using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private readonly ITariffRepository _dbTariff;
        private readonly IMapper _mapper;

        public TariffsController(ITariffRepository dbTariff, IMapper mapper)
        {
            _dbTariff = dbTariff;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TariffDTO>>> GetTariffs()
        {
            IEnumerable<Tariff> tariffList = await _dbTariff.GetAllTariffsWithCityAsync();
            return Ok(_mapper.Map<List<TariffDTO>>(tariffList));
        }

        [HttpGet("{id:int}", Name = "GetTariff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TariffDTO>> GetTariff(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var tariff = await _dbTariff.GetAsync(u => u.TariffID == id);
            if (tariff == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TariffDTO>(tariff));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TariffDTO>> SearchTariffs(string cityName)
        {
            var tariffs = await _dbTariff.SearchTariffsAsync(cityName);
            return Ok(_mapper.Map<List<TariffDTO>>(tariffs));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TariffDTO>> CreateTariff([FromBody] TariffCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }

            if (await _dbTariff.GetAsync(u => u.CityID == createDTO.CityID) != null)
            {
                ModelState.AddModelError("CustomError", "Tariff already Exists!");
                return BadRequest();
            }

            Tariff tariff = _mapper.Map<Tariff>(createDTO);
            await _dbTariff.CreateAsync(tariff);

            return CreatedAtRoute("GetTariff", new { id = tariff.TariffID }, tariff);
        }

        [HttpPut("{id:int}", Name = "UpdateTariff")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTariff(int id, [FromBody] TariffDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.TariffID)
            {
                return BadRequest();
            }

            Tariff model = _mapper.Map<Tariff>(updateDTO);
            await _dbTariff.UpdateAsync(model);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteTariff")]
        public async Task<IActionResult> DeleteTariff(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var tariff = await _dbTariff.GetAsync(u => u.TariffID == id);
            if (tariff == null)
            {
                return NotFound();
            }
            await _dbTariff.RemoveAsync(tariff);
            return NoContent();
        }
    }
}
