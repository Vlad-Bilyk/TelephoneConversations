using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TelephoneConversations.Core.Models.DTOs;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _dbDiscount;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountRepository dbDiscount, IMapper mapper)
        {
            _dbDiscount = dbDiscount;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DiscountDTO>>> GetDiscounts()
        {
            IEnumerable<Discount> discountList = await _dbDiscount.GetAllAsync();
            return Ok(_mapper.Map<List<DiscountDTO>>(discountList));
        }

        [HttpGet("{id:int}", Name = "GetDiscount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DiscountDTO>> GetDiscount(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var discount = await _dbDiscount.GetAsync(u => u.DiscountID == id);
            if (discount == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DiscountDTO>(discount));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DiscountDTO>> SearchDiscounts(int tariffId)
        {
            var discounts = await _dbDiscount.SearchDiscountsAsync(tariffId);
            return Ok(_mapper.Map<List<DiscountDTO>>(discounts));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DiscountDTO>> CreateDiscount([FromBody] DiscountCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }

            if (await _dbDiscount.GetAsync(u => u.DurationMin == createDTO.DurationMin) != null)
            {
                return BadRequest();
            }

            Discount discount = _mapper.Map<Discount>(createDTO);
            await _dbDiscount.CreateAsync(discount);

            return CreatedAtRoute("GetDiscount", new { id = discount.DiscountID }, discount);
        }

        [HttpPut("{id:int}", Name = "UpdateDiscount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDiscount(int id, [FromBody] DiscountDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.DiscountID)
            {
                return BadRequest();
            }

            Discount model = _mapper.Map<Discount>(updateDTO);
            await _dbDiscount.UpdateAsync(model);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteDiscount")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var discount = await _dbDiscount.GetAsync(u => u.DiscountID == id);
            if (discount == null)
            {
                return NotFound();
            }
            await _dbDiscount.RemoveAsync(discount);
            return NoContent();
        }
    }
}