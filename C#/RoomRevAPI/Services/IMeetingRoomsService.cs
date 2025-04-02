
    using RoomRevAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace RoomRevAPI.Services
    {
        public interface IMeetingRoomsService
        {
        Task<List<MeetingRooms>> GetAllMeetingRoomsAsync();
        Task<MeetingRooms?> GetMeetingRoomByIdAsync(Guid id);
        Task CreateMeetingRoomAsync(MeetingRooms room);
        Task<bool> UpdateMeetingRoomAsync(Guid id, MeetingRooms room);
        Task<bool> DeleteMeetingRoomAsync(Guid id);
    }
    }

