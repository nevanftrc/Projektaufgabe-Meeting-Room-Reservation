using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RoomRevAPI.Models
{
    public class ReserverDTO
    {
        public Guid RevNr { get; set; }
        public string? Name { get; set; }
    }
}
