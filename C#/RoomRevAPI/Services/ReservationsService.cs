using MongoDB.Driver;
using RoomRevAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomRevAPI.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IMongoCollection<Reservations> _reservationsCollection;

        public ReservationsService(MainController dbContext)
        {
            _reservationsCollection = dbContext.Reservations;
        }

        public async Task<List<Reservations>> GetAllReservationsAsync()
        {
            return await _reservationsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Reservations?> GetReservationByIdAsync(Guid id)
        {
            return await _reservationsCollection.Find(r => r.RevRoomMet == id).FirstOrDefaultAsync();
        }

        public async Task CreateReservationAsync(Reservations reservation)
        {
            await _reservationsCollection.InsertOneAsync(reservation);
        }

        public async Task<bool> UpdateReservationAsync(Guid id, Reservations reservation)
        {
            reservation.RevRoomMet = id;
            var result = await _reservationsCollection.ReplaceOneAsync(r => r.RevRoomMet == id, reservation);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteReservationAsync(Guid id)
        {
            var result = await _reservationsCollection.DeleteOneAsync(r => r.RevRoomMet == id);
            return result.DeletedCount > 0;
        }
    }
}
