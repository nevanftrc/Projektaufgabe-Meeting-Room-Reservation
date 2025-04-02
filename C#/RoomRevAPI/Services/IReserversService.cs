using RoomRevAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomRevAPI.Services
{
    public interface IReserversService
    {
        Task<List<Reservers>> GetAllReserversAsync();
        Task<Reservers?> GetReserverByIdAsync(Guid id);
        Task CreateReserverAsync(Reservers reserver);
        Task<bool> UpdateReserverAsync(Guid id, Reservers reserver);
        Task<bool> DeleteReserverAsync(Guid id);
    }
}
