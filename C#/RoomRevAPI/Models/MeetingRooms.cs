using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RoomRevAPI.Models
{
    public class MeetingRooms
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid RoomRevNr { get; set; } = Guid.NewGuid();

        [BsonElement("Availability")]
        public bool Availability { get; set; }

        [BsonElement("Capacity")]
        public int Capacity { get; set; }

        [BsonElement("RoomName")]
        public string? RoomName { get; set; }

        [BsonElement("Equipment")]
        public List<Tools>? Equipment { get; set; } = new List<Tools>();

        [BsonIgnore]
        public ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
    }

    public class Tools
    {
        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Description")]
        public string? Description { get; set; }

        [BsonElement("Count")]
        public int Count { get; set; }
    }
}
