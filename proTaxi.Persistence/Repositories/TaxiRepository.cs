

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using proTaxi.Domain.Entities;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Context;
using proTaxi.Persistence.Exceptions;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Taxi;
using proTaxi.Persistence.Repository;

namespace proTaxi.Persistence.Repositories
{
    public sealed class TaxiRepository : BaseRepository<Taxi, int>, ITaxiRepository
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

        public async Task<DataResult<TaxiModel>> GetTaxiByPlaca(string placa)
        {
            DataResult<TaxiModel> result = new DataResult<TaxiModel>();
            try
            {
                var taxi = await this.taxiDb.Taxi
                                             .SingleOrDefaultAsync(taxi => taxi.Placa == placa
                                                                   && taxi.Deleted == false);

                if (taxi == null)
                {
                    result.Message = "El taxi no se encuentra registrado o fue eliminado";
                    result.Success = false;
                }
                result.Result = new TaxiModel()
                {
                    Id = taxi.Id,
                    Placa = taxi.Placa,
                };
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["taxi:error_get_taxi_placa"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<DataResult<List<TaxiModel>>> GetTaxis()
        {
            DataResult<List<TaxiModel>> result = new DataResult<List<TaxiModel>>();

            try
            {
                var taxis = await (from taxi in this.taxiDb.Taxi
                                   where taxi.Deleted == false
                                   select new TaxiModel()
                                   {
                                       Placa = taxi.Placa,
                                       Id = taxi.Id
                                   }).ToListAsync();


                result.Result = taxis;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["taxi:get_taxis"];
                result.Success = false;
                this.logger.LogError(this.configuration["taxi:get_taxis"], ex.ToString());
            }
            return result;
        }
        public override async Task<bool> Save(Taxi entity)
        {
            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["taxi:entity"]);
                }

                if (await base.Exists(taxi => taxi.Placa == entity.Placa))
                    throw new TaxiDataException(this.configuration["taxi:placa_exists"]);

                if (string.IsNullOrEmpty(entity.Placa))
                {
                    throw new TaxiDataException(this.configuration["taxi:placa_is_null"]);
                }
                if (entity.Placa.Length > 50)
                {
                    throw new TaxiDataException(this.configuration["taxi:placa_length"]);
                }
                
                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["taxi:error_save"], ex.ToString());
            }
            return result;
        
    }
        public override async Task<bool> Update(Taxi entity)
        {
            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["taxi:entity"]);
                }

                if (string.IsNullOrEmpty(entity.Placa))
                {
                    throw new TaxiDataException(this.configuration["taxi:placa_is_null"]);
                }
                if (entity.Placa.Length > 50)
                {
                    throw new TaxiDataException(this.configuration["taxi:placa_length"]);
                }

                Taxi? TaxiToUpdate = this.taxiDb.Taxi.Find(entity.Id);

                TaxiToUpdate.Placa = entity.Placa;
                TaxiToUpdate.ModifyDate = entity.ModifyDate;
                TaxiToUpdate.ModifyUser = entity.ModifyUser;

                result = await base.Update(TaxiToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["taxi:error_update"], ex.ToString());
            }
            return result;
        }
    }
}
