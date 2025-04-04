using RoomRevAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomRevAPI.Services
{
    public interface IReservationsService
    {
        Task<List<Reservations>> GetAllReservationsAsync();
        Task<Reservations?> GetReservationByIdAsync(Guid id);
        Task<bool> CreateReservationAsync(Reservations reservation);
        Task<bool> UpdateReservationAsync(Guid id, Reservations reservation);
        Task<bool> DeleteReservationAsync(Guid id);
    }
}
