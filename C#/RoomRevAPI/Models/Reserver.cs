using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RoomRevAPI.Models
{
    public class Reservers
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid RevNr { get; set; } = Guid.NewGuid();
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonIgnore]
        public ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
    }
}
