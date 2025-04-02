using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomRevAPI.Models;
using RoomRevAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomRevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRoomsController : ControllerBase
    {
        private readonly IMeetingRoomsService _meetingRoomsService;
        private readonly IMapper _mapper;

        public MeetingRoomsController(IMeetingRoomsService meetingRoomsService, IMapper mapper)
        {
            _meetingRoomsService = meetingRoomsService;
            _mapper = mapper;
        }

        // ✅ Get all meeting rooms
        [HttpGet]
        public async Task<ActionResult<List<MeetingRoomDTO>>> GetMeetingRooms()
        {
            var rooms = await _meetingRoomsService.GetAllMeetingRoomsAsync();
            return Ok(_mapper.Map<List<MeetingRoomDTO>>(rooms));
        }

        // ✅ Get a single meeting room by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingRoomDTO>> GetMeetingRoom(Guid id)
        {
            var room = await _meetingRoomsService.GetMeetingRoomByIdAsync(id);
            if (room == null)
                return NotFound(new { message = "Meeting room not found" });

            return Ok(_mapper.Map<MeetingRoomDTO>(room));
        }

        // ✅ Create a new meeting room
        [HttpPost]
        public async Task<ActionResult<MeetingRoomDTO>> CreateMeetingRoom([FromBody] MeetingRoomDTO roomDto)
        {
            if (roomDto == null)
                return BadRequest(new { message = "Invalid meeting room data" });

            var room = _mapper.Map<MeetingRooms>(roomDto);
            await _meetingRoomsService.CreateMeetingRoomAsync(room);

            return CreatedAtAction(nameof(GetMeetingRoom), new { id = room.RoomRevNr }, _mapper.Map<MeetingRoomDTO>(room));
        }

        // ✅ Update a meeting room
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeetingRoom(Guid id, [FromBody] MeetingRoomDTO roomDto)
        {
            if (roomDto == null)
                return BadRequest(new { message = "Invalid meeting room data" });

            var updated = await _meetingRoomsService.UpdateMeetingRoomAsync(id, _mapper.Map<MeetingRooms>(roomDto));
            if (!updated)
                return NotFound(new { message = "Meeting room not found" });

            return NoContent();
        }

        // ✅ Delete a meeting room
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetingRoom(Guid id)
        {
            var deleted = await _meetingRoomsService.DeleteMeetingRoomAsync(id);
            if (!deleted)
                return NotFound(new { message = "Meeting room not found" });

            return NoContent();
        }
    }
}
