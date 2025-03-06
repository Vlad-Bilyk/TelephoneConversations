using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TelephoneConversations.Core.Interfaces;
using TelephoneConversations.Core.Models;
using TelephoneConversations.Core.Models.DTOs;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        private readonly ICallService _callService;
        private readonly IMapper _mapper;

        public CallController(ICallService callService, IMapper mapper)
        {
            _callService = callService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CallDTO>>> GetCalls()
        {
            IEnumerable<Call> callList = await _callService.GetAllAsync();
            return Ok(_mapper.Map<List<CallDTO>>(callList));
        }

        [HttpGet("{id:int}", Name = "GetCall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CallDTO>> GetCall(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var call = await _callService.GetAsync(u => u.CallID == id);
            if (call == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CallDTO>(call));
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CallDTO>> SearchCalls(string? subscriberName, string? cityName)
        {
            var calls = await _callService.SearchСallsAsync(cityName, subscriberName);
            return Ok(_mapper.Map<List<CallDTO>>(calls));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CallDTO>> CreateCall([FromBody] CallCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }

            Call callEntity = _mapper.Map<Call>(createDTO);
            Call createdCall = await _callService.CreateCallAsync(callEntity);
            CallDTO callDto = _mapper.Map<CallDTO>(createdCall);

            return CreatedAtAction(nameof(GetCall), new { id = callDto.CallID }, callDto);
        }
    }
}
