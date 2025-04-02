using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace RoomRevAPI.Models
{
    public class Reservations
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid RevRoomMet { get; set; } = default(Guid);
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RevNr")]
        public Guid RevNr { get; set; }
        [BsonRepresentation(BsonType.String)]
        [BsonElement("RoomRevNr")]
        public Guid RoomRevNr { get; set; }
        [BsonIgnore]
        public MeetingRooms? MeetingRooms { get; set; }
        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }
        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }
        [StringLength(50)]
        [BsonElement("Reason")]
        public string? Reason { get; set; }
        [BsonIgnore]
        public ICollection<Reservers> Reservers { get; set; } = new List<Reservers>();
    }
}
