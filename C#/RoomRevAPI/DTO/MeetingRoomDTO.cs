using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RoomRevAPI.Models
{
    public class MeetingRoomDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RoomRevNr")]
        public Guid RoomRevNr { get; set; }

        [BsonElement("Availability")]
        public bool Availability { get; set; }

        [BsonElement("Capacity")]
        public int Capacity { get; set; }

        [BsonElement("RoomName")]
        public string? RoomName { get; set; }

        [BsonElement("Tools")]
        public List<ToolDTO>? Tools { get; set; } = new List<ToolDTO>();
    }

    public class ToolDTO
    {
        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("Count")]
        public int Count { get; set; }
    }
}
