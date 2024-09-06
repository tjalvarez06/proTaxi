

using proTaxi.Domain.Entities;
using proTaxi.Domain.Interfaces;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Context;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Viaje;
using proTaxi.Persistence.Repository;
using System.Linq.Expressions;

namespace proTaxi.Persistence.Repositories
{
    public sealed class ViajeRepository : BaseRepository<Viaje, int>, IViajeRepository
    {
        public ViajeRepository(TaxiDb taxiDb) : base(taxiDb) 
        {
        
        }

        public Task<DataResult<ViajeModel>> GetViajes()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<ViajeModel>> GetViajes(string name)
        {
            throw new NotImplementedException();
        }
    }
}
