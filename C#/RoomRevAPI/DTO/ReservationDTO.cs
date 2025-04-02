using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace RoomRevAPI.Models
{
    public class ReservationDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RevRoomMet")]
        public Guid RevRoomMet { get; set; }
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RevNr")]
        public Guid RevNr { get; set; }
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RoomRevNr")]
        public Guid RoomRevNr { get; set; }
        public DateTime StartTime { get; set; }
        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }
        [StringLength(50)]
        [BsonElement("Reason")]
        public string? Reason { get; set; }
    }
}
