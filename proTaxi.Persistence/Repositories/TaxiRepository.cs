

using Microsoft.EntityFrameworkCore;
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

        public async Task<DataResult<List<TaxiModel>>> GetTaxiByViaje(int viajeId)
        {
            DataResult<List<TaxiModel>> result = new DataResult<List<TaxiModel>>();

            try
            {
                var query = await (from taxi in this.taxiDb.Taxi
                                   join viaje in this.taxiDb.Viaje on taxi.Id equals viaje.Id
                                   where taxi.Deleted == false
                                    && taxi.Id == viajeId
                                   select new TaxiModel()
                                   {
                                       Placa = taxi.Placa,
                                       Id = viaje.Id
                                   }).ToListAsync();


                result.Result = query;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["course:get_taxi_viaje_id"];
                result.Success = false;
                this.logger.LogError(this.configuration["course:get_taxi_viaje_id"], ex.ToString());
            }
            return result;
        }

        public Task<DataResult<TaxiModel>> GetTaxiByPlaca(string placa)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<TaxiModel>>> GetTaxis()
        {
            DataResult<List<TaxiModel>> result = new DataResult<List<TaxiModel>>();

            try
            {
                var query = await (from taxi in this.taxiDb.Taxi
                                   join viaje in this.taxiDb.Viaje on taxi.Id equals viaje.Id
                                   where taxi.Deleted == false
                                   select new TaxiModel()
                                   {
                                       Placa = taxi.Placa,
                                       Id = viaje.Id
                                   }).ToListAsync();


                result.Result = query;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["taxi:get_taxis"];
                result.Success = false;
                this.logger.LogError(this.configuration["taxi:get_taxis"], ex.ToString());
            }
            return result;
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
