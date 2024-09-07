

using proTaxi.Domain.Entities;
using proTaxi.Domain.Interfaces;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Models.Viaje;

namespace proTaxi.Persistence.Interfaces
{
    public interface IViajeRepository : IRepository<Viaje, int>
    {
        Task<DataResult<List<ViajeModel>>> GetViajes();
        Task<DataResult<ViajeModel>> GetViajes(string name);
        
    }
}
