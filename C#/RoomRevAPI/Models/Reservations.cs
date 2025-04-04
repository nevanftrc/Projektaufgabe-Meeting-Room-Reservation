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

        public bool IsTimeValid()
        {
            // Check if the start time is not in the past
            if (StartTime < DateTime.Now)
            {
                throw new ArgumentException("Start time cannot be in the past.");
            }

            // Check if the start time is before the end time
            if (StartTime >= EndTime)
            {
                throw new ArgumentException("Start time must be before end time.");
            }

            return true;
        }
    }
}
