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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationsService reservationsService, IMapper mapper)
        {
            _reservationsService = reservationsService;
            _mapper = mapper;
        }

        // ✅ Get all reservations
        [HttpGet]
        public async Task<ActionResult<List<ReservationDTO>>> GetReservations()
        {
            var reservations = await _reservationsService.GetAllReservationsAsync();
            return Ok(_mapper.Map<List<ReservationDTO>>(reservations));
        }

        // ✅ Get a reservation by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(Guid id)
        {
            var reservation = await _reservationsService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound(new { message = "Reservation not found" });

            return Ok(_mapper.Map<ReservationDTO>(reservation));
        }

        // ✅ Create a new reservation
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> CreateReservation([FromBody] ReservationDTO reservationDto)
        {
            if (reservationDto == null)
                return BadRequest(new { message = "Invalid reservation data" });

            var reservation = _mapper.Map<Reservations>(reservationDto);
            await _reservationsService.CreateReservationAsync(reservation);

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.RevRoomMet }, _mapper.Map<ReservationDTO>(reservation));
        }

        // ✅ Update a reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] ReservationDTO reservationDto)
        {
            if (reservationDto == null)
                return BadRequest(new { message = "Invalid reservation data" });

            var updated = await _reservationsService.UpdateReservationAsync(id, _mapper.Map<Reservations>(reservationDto));
            if (!updated)
                return NotFound(new { message = "Reservation not found" });

            return NoContent();
        }

        // ✅ Delete a reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            var deleted = await _reservationsService.DeleteReservationAsync(id);
            if (!deleted)
                return NotFound(new { message = "Reservation not found" });

            return NoContent();
        }
    }
}
