using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomRevAPI.Models;
using RoomRevAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomRevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserversController : ControllerBase
    {
        private readonly IReserversService _reserversService;
        private readonly IMapper _mapper;

        public ReserversController(IReserversService reserversService, IMapper mapper)
        {
            _reserversService = reserversService;
            _mapper = mapper;
        }

        // Get all reservers
        [HttpGet]
        public async Task<ActionResult<List<ReserverDTO>>> GetReservers()
        {
            var reservers = await _reserversService.GetAllReserversAsync();
            return Ok(_mapper.Map<List<ReserverDTO>>(reservers));
        }

        // Get a reserver by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ReserverDTO>> GetReserver(Guid id)
        {
            var reserver = await _reserversService.GetReserverByIdAsync(id);
            if (reserver == null)
                return NotFound(new { message = "Reserver not found" });

            return Ok(_mapper.Map<ReserverDTO>(reserver));
        }

        // Create a new reserver
        [HttpPost]
        public async Task<ActionResult<ReserverDTO>> CreateReserver([FromBody] ReserverDTO reserverDto)
        {
            if (reserverDto == null)
                return BadRequest(new { message = "Invalid reserver data" });

            var reserver = _mapper.Map<Reservers>(reserverDto);
            await _reserversService.CreateReserverAsync(reserver);

            return CreatedAtAction(nameof(GetReserver), new { id = reserver.RevNr }, _mapper.Map<ReserverDTO>(reserver));
        }

        // Update a reserver
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReserver(Guid id, [FromBody] ReserverDTO reserverDto)
        {
            if (reserverDto == null)
                return BadRequest(new { message = "Invalid reserver data" });

            var updated = await _reserversService.UpdateReserverAsync(id, _mapper.Map<Reservers>(reserverDto));
            if (!updated)
                return NotFound(new { message = "Reserver not found" });

            return NoContent();
        }

        // Delete a reserver
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserver(Guid id)
        {
            var deleted = await _reserversService.DeleteReserverAsync(id);
            if (!deleted)
                return NotFound(new { message = "Reserver not found" });

            return NoContent();
        }
    }
}
