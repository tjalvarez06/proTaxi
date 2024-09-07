

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using proTaxi.Domain.Entities;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Context;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Taxi;
using proTaxi.Persistence.Repository;

namespace proTaxi.Persistence.Repositories
{
    public class TaxiRepository : BaseRepository<Taxi, int>, ITaxiRepository
    {
        private readonly TaxiDb taxiDb;
        private readonly ILogger<TaxiRepository> logger;
        private readonly IConfiguration configuration;

        public TaxiRepository(TaxiDb taxiDb,
                              ILogger<TaxiRepository> logger,
                              IConfiguration configuration) : base(taxiDb)
        {
            this.taxiDb = taxiDb;
            this.logger = logger;
            this.configuration = configuration;
        }

        public Task<DataResult<TaxiModel>> GetTaxiByHasta(string viajeHasta)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<TaxiModel>> GetTaxiByPlaca(string placa)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<TaxiModel>>> GetTaxis()
        {
            throw new NotImplementedException();
        }
        public override Task<bool> Save(Taxi entity)
        {
            return base.Save(entity);
        }
        public override Task<bool> Update(Taxi entity)
        {
            return base.Update(entity);
        }
    }
}
