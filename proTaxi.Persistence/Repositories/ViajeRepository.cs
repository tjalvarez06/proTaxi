

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using proTaxi.Domain.Entities;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Context;
using proTaxi.Persistence.Exceptions;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Viaje;
using proTaxi.Persistence.Repository;

namespace proTaxi.Persistence.Repositories
{
    public sealed class ViajeRepository : BaseRepository<Viaje, int>, IViajeRepository
    {
        private readonly TaxiDb taxiDb;
        private readonly ILogger<ViajeRepository> logger;
        private readonly IConfiguration configuration;

        public ViajeRepository(TaxiDb taxiDb,
                               ILogger<ViajeRepository> logger,
                               IConfiguration configuration) : base(taxiDb)
        {
            this.taxiDb = taxiDb;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<DataResult<List<ViajeModel>>> GetViajes()
        {
            DataResult<List<ViajeModel>> result = new DataResult<List<ViajeModel>>();
            try
            {
                var viajes = await (from viaje in this.taxiDb.Viaje
                             where viaje.Deleted == false
                             select new ViajeModel
                             {
                                 Id = viaje.Id,
                                 FechaInicio = viaje.FechaInicio.Value,
                                 FechaFin = viaje.FechaFin,
                                 Desde = viaje.Desde,
                                 Hasta = viaje.Hasta,
                                 Calificacion = viaje.Calificacion,
                                 TaxiId = viaje.TaxiId,
                                 UsuarioId = viaje.UsuarioId,
    }).ToListAsync();

                result.Result = viajes;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = this.configuration["viaje: error_get_viaje"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<DataResult<ViajeModel>> GetViajes(string hasta)
        {
            DataResult<ViajeModel> result = new DataResult<ViajeModel>();
            try
            {
                var viaje = await this.taxiDb.Viaje
                                             .SingleOrDefaultAsync(viaje => viaje.Hasta == hasta
                                                                   && viaje.Deleted == false);

                if (viaje == null)
                {
                    result.Message = "El viaje no se encuentra registrado o fue eliminado";
                    result.Success = false;
                }
                result.Result = new ViajeModel()
                {
                    Id = viaje.Id,
                    FechaInicio = viaje.FechaInicio.Value,
                    FechaFin = viaje.FechaFin,
                    Desde = viaje.Desde,
                    Hasta = viaje.Hasta,
                    Calificacion = viaje.Calificacion,
                    TaxiId = viaje.TaxiId,
                    UsuarioId = viaje.UsuarioId,
                };
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["viaje:error_get_viaje_hasta"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public override async Task<bool> Save(Viaje entity)
        {
            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["viaje:entity"]);
                }

                if (await base.Exists(viaje => viaje.Hasta == entity.Hasta))
                    throw new ViajeDataException(this.configuration["viaje:hasta_exists"]);

                if (string.IsNullOrEmpty(entity.Hasta))
                {
                    throw new ViajeDataException(this.configuration["viaje:hasta_is_null"]);
                }
                if (entity.Hasta.Length > 50)
                {
                    throw new ViajeDataException(this.configuration["viaje:hasta_length"]);
                }
                if (!entity.FechaInicio.HasValue)
                {
                    throw new ViajeDataException(this.configuration["Viaje:fecha_inicio_is_null"]);
                }

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["viaje:error_save"], ex.ToString());
            }
            return result;
        }
        public override async Task<bool> Update(Viaje entity)
        {

            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["viaje:entity"]);
                }
                if (string.IsNullOrEmpty(entity.Hasta))
                {
                    throw new ViajeDataException(this.configuration["viaje:hasta_is_null"]);
                }
                if (entity.Hasta.Length > 50)
                {
                    throw new ViajeDataException(this.configuration["viaje:hasta_length"]);
                }
                if (!entity.FechaInicio.HasValue)
                {
                    throw new ViajeDataException(this.configuration["Viaje:fecha_inicio_is_null"]);
                }
                Viaje? viajeToUpdate = this.taxiDb.Viaje.Find(entity.Id);

                viajeToUpdate.FechaInicio = entity.FechaInicio.Value;
                viajeToUpdate.FechaFin = entity.FechaFin;
                viajeToUpdate.Desde = entity.Desde;
                viajeToUpdate.Hasta = entity.Hasta;
                viajeToUpdate.Calificacion = entity.Calificacion;
                viajeToUpdate.TaxiId = entity.TaxiId;
                viajeToUpdate.UsuarioId = entity.UsuarioId;
                viajeToUpdate.ModifyDate = entity.ModifyDate;
                viajeToUpdate.ModifyUser = entity.ModifyUser;

                result = await base.Update(viajeToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["viaje:error_update"], ex.ToString());
            }
            return result;
        }
    }
}
