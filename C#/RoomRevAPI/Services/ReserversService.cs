using MongoDB.Driver;
using RoomRevAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomRevAPI.Services
{
    public class ReserversService : IReserversService
    {
        private readonly IMongoCollection<Reservers> _reserversCollection;

        public ReserversService(MainController dbContext)
        {
            _reserversCollection = dbContext.Reservers;
        }

        public async Task<List<Reservers>> GetAllReserversAsync()
        {
            return await _reserversCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Reservers?> GetReserverByIdAsync(Guid id)
        {
            return await _reserversCollection.Find(r => r.RevNr == id).FirstOrDefaultAsync();
        }

        public async Task CreateReserverAsync(Reservers reserver)
        {
            await _reserversCollection.InsertOneAsync(reserver);
        }

        public async Task<bool> UpdateReserverAsync(Guid id, Reservers reserver)
        {
            reserver.RevNr = id;
            var result = await _reserversCollection.ReplaceOneAsync(r => r.RevNr == id, reserver);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteReserverAsync(Guid id)
        {
            var result = await _reserversCollection.DeleteOneAsync(r => r.RevNr == id);
            return result.DeletedCount > 0;
        }
    }
}
