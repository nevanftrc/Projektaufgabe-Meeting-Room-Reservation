using MongoDB.Driver;
using RoomRevAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomRevAPI.Services
{
    public class MeetingRoomsService : IMeetingRoomsService
    {
        private readonly IMongoCollection<MeetingRooms> _meetingRoomsCollection;

        public MeetingRoomsService(MainController dbContext)
        {
            _meetingRoomsCollection = dbContext.MeetingRooms;
        }

        public async Task<List<MeetingRooms>> GetAllMeetingRoomsAsync()
        {
            return await _meetingRoomsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<MeetingRooms?> GetMeetingRoomByIdAsync(Guid id)
        {
            return await _meetingRoomsCollection.Find(r => r.RoomRevNr == id).FirstOrDefaultAsync();
        }

        public async Task CreateMeetingRoomAsync(MeetingRooms room)
        {
            await _meetingRoomsCollection.InsertOneAsync(room);
        }

        public async Task<bool> UpdateMeetingRoomAsync(Guid id, MeetingRooms room)
        {
            room.RoomRevNr = id;
            var result = await _meetingRoomsCollection.ReplaceOneAsync(r => r.RoomRevNr == id, room);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteMeetingRoomAsync(Guid id)
        {
            var result = await _meetingRoomsCollection.DeleteOneAsync(r => r.RoomRevNr == id);
            return result.DeletedCount > 0;
        }
    }
}