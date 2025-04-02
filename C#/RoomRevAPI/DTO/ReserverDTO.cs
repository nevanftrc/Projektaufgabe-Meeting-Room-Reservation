using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RoomRevAPI.Models
{
    public class ReserverDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RevNr")]
        public Guid RevNr { get; set; }
        [BsonElement("Name")]
        public string? Name { get; set; }
    }
}
