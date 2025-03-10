﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.DTOs;
using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberRepository _dbSubscriber;
        private readonly IMapper _mapper;

        public SubscribersController(ISubscriberRepository dbSubscriber, IMapper mapper)
        {
            _dbSubscriber = dbSubscriber;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SubscriberDTO>>> GetSubscribers()
        {
            IEnumerable<Subscriber> subscriberList = await _dbSubscriber.GetAllAsync();
            return Ok(_mapper.Map<List<SubscriberDTO>>(subscriberList));
        }

        [HttpGet("{id:int}", Name = "GetSubscriber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubscriberDTO>> GetSubscriber(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var subscriber = await _dbSubscriber.GetAsync(u => u.SubscriberID == id);
            if (subscriber == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SubscriberDTO>(subscriber));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SubscriberDTO>> SearchSubscribers(string companyName)
        {
            var subscribers = await _dbSubscriber.SearchSubscribersAsync(companyName);
            return Ok(_mapper.Map<List<SubscriberDTO>>(subscribers));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubscriberDTO>> CreateSubscriber([FromBody] SubscriberCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }

            if (!Regex.IsMatch(createDTO.IPN, @"^\d{10}$"))
            {
                return BadRequest("ІПН має містити рівно 10 цифр.");
            }

            if (!Regex.IsMatch(createDTO.TelephonePoint, @"^\+380\d{9}$"))
            {
                return BadRequest("Номер телефону має бути у форматі +380XXXXXXXXX.");
            }

            if (await _dbSubscriber.GetAsync(u => u.CompanyName.ToLower() == createDTO.CompanyName.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Subscriber already Exists!");
                return BadRequest();
            }

            Subscriber subscriber = _mapper.Map<Subscriber>(createDTO);
            await _dbSubscriber.CreateAsync(subscriber);

            return CreatedAtRoute("GetSubscriber", new { id = subscriber.SubscriberID }, subscriber);
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
            await _dbSubscriber.UpdateAsync(model);
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
            var subscriber = await _dbSubscriber.GetAsync(u => u.SubscriberID == id);
            if (subscriber == null)
            {
                return NotFound();
            }
            await _dbSubscriber.RemoveAsync(subscriber);
            return NoContent();
        }
    }
}
