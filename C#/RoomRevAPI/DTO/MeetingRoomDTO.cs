using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RoomRevAPI.Models
{
    public class MeetingRoomDTO
    {
        public Guid RoomRevNr { get; set; }
        public bool Availability { get; set; }
        public int Capacity { get; set; }
        public string? RoomName { get; set; }

        public List<ToolDTO>? Equipment { get; set; } = new List<ToolDTO>();
    }

    public class ToolDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
    }
}
