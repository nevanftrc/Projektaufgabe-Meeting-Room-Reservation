using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RoomRevAPI.Models
{
    public class Reservers
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RevNr")]
        public Guid RevNr { get; set; }
        [BsonElement("Name")]
        public string? Name { get; set; }
        public ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
    }
}
