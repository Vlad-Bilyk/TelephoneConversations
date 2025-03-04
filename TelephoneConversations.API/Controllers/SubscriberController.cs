using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelephoneConversations.API.DTOs;
using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SubscriberController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SubscriberDTO>>> GetSubscribers()
        {
            IEnumerable<Subscriber> subscriberList = await _db.Subscribers.ToListAsync();
            return Ok(_mapper.Map<List<SubscriberDTO>>(subscriberList));
        }

        [HttpGet("{id:int}", Name = "GetSubscriber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSubscriber(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var subscriber = await _db.Subscribers.FirstOrDefaultAsync(u => u.SubscriberID == id);
            if (subscriber == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SubscriberDTO>(subscriber));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubscriberDTO>> CreateSubscriber([FromBody] SubscriberCreateDTO createDTO)
        {
            if (await _db.Subscribers.FirstOrDefaultAsync(u => u.CompanyName.ToLower() == createDTO.CompanyName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Subscriber already Exists!");
                return BadRequest();
            }

            if (createDTO == null)
            {
                return BadRequest();
            }

            Subscriber model = _mapper.Map<Subscriber>(createDTO);
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetSubscriber", new { id = model.SubscriberID }, model);
        }

        [HttpPut("{id:int}", Name = "UpdateSubscriber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSubscriber(int id, [FromBody] SubscriberUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.SubscriberID)
            {
                return BadRequest();
            }

            Subscriber model = _mapper.Map<Subscriber>(updateDTO);
            _db.Subscribers.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteSubscriber")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var subscriber = await _db.Subscribers.FirstOrDefaultAsync(u => u.SubscriberID == id);
            if (subscriber == null)
            {
                return NotFound();
            }
            _db.Subscribers.Remove(subscriber);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
