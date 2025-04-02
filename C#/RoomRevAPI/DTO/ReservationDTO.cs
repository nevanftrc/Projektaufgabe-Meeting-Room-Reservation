using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace RoomRevAPI.Models
{
    public class ReservationDTO
    {
        public Guid RevRoomMet { get; set; }
        public Guid RevNr { get; set; }
        public Guid RoomRevNr { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Reason { get; set; }
    }
}
