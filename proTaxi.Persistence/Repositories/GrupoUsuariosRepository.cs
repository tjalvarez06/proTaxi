

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using proTaxi.Domain.Entities;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Context;
using proTaxi.Persistence.Exceptions;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.GrupoUsuarios;
using proTaxi.Persistence.Repository;

namespace proTaxi.Persistence.Repositories
{
    public sealed class GrupoUsuariosRepository : BaseRepository<GrupoUsuarios, int>, IGrupoUsuariosRepository
    {
        private readonly TaxiDb taxiDb;
        private readonly ILogger<TaxiRepository> logger;
        private readonly IConfiguration configuration;

        public GrupoUsuariosRepository(TaxiDb taxiDb,
                              ILogger<TaxiRepository> logger,
                              IConfiguration configuration) : base(taxiDb)
        {
            this.taxiDb = taxiDb;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<DataResult<List<GrupoUsuariosModel>>> GetGrupoUsuariosByUsuario(int usuarioId)
        {
            DataResult<List<GrupoUsuariosModel>> result = new DataResult<List<GrupoUsuariosModel>>();

            try
            {
                var query = await (from grupoUsuarios in this.taxiDb.GrupoUsuarios
                                   join usuario in this.taxiDb.Usuario on grupoUsuarios.Id equals usuario.Id
                                   where grupoUsuarios.Deleted == false
                                    && grupoUsuarios.Id == usuarioId
                                   select new GrupoUsuariosModel()
                                   {
                                      Id = usuario.Id
                                   }).ToListAsync();


                result.Result = query;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["GrupoUsuario:get_GrupoUsuario_Usuario_id"];
                result.Success = false;
                this.logger.LogError(this.configuration["GrupoUsuario:get_GrupoUsuario_Usuario_id"], ex.ToString());
            }
            return result;
        }

        public async Task<DataResult<GrupoUsuariosModel>> GetGrupoUsuarioById(int id)
        {
            DataResult<GrupoUsuariosModel> result = new DataResult<GrupoUsuariosModel>();
            try
            {
                var grupoUsuarios = await this.taxiDb.GrupoUsuarios
                                             .SingleOrDefaultAsync(grupoUsuarios => grupoUsuarios.Id == id
                                                                   && grupoUsuarios.Deleted == false);

                if (grupoUsuarios == null)
                {
                    result.Message = "El Grupo de usuarios no se encuentra registrado o fue eliminado";
                    result.Success = false;
                }
                result.Result = new GrupoUsuariosModel()
                {
                    Id = grupoUsuarios.Id,
                };
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["grupoUsuarios:error_get_grupousuarios_Id"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<DataResult<List<GrupoUsuariosModel>>> GetGruposUsuarios()
        {
            DataResult<List<GrupoUsuariosModel>> result = new DataResult<List<GrupoUsuariosModel>>();

            try
            {
                var grupoUsuarios = await (from grupoUsuario in this.taxiDb.GrupoUsuarios
                                   where grupoUsuario.Deleted == false
                                   select new GrupoUsuariosModel()
                                   {
                                       Id = grupoUsuario.Id
                                   }).ToListAsync();


                result.Result = grupoUsuarios;
            }
            catch (Exception ex)
            {

                result.Message = this.configuration["grupoUsuario:get_grupoUsuario"];
                result.Success = false;
                this.logger.LogError(this.configuration["grupoUsuario:get_grupoUsuarios"], ex.ToString());
            }
            return result;
        }
        public override async Task<bool> Save(GrupoUsuarios entity)
        {
            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["grupoUsuario:entity"]);
                }

                if (await base.Exists(grupoUsuario => grupoUsuario.Id == entity.Id))
                    throw new GrupoUsuariosDataException(this.configuration["grupoUsuario:placa_exists"]);

                if (entity.Id == null)
                {
                    throw new GrupoUsuariosDataException(this.configuration["grupoUsuario:placa_is_null"]);
                }
                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["grupoUsuario:error_save"], ex.ToString());
            }
            return result;

        }
        public override async Task<bool> Update(GrupoUsuarios entity)
        {
            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["grupoUsuario:entity"]);
                }

                if (entity.Id == null)
                {
                    throw new GrupoUsuariosDataException(this.configuration["grupoUsuario:placa_is_null"]);
                }

                GrupoUsuarios? GrupoUsuarioToUpdate = this.taxiDb.GrupoUsuarios.Find(entity.Id);

                GrupoUsuarioToUpdate.Id = entity.Id;
                GrupoUsuarioToUpdate.ModifyDate = entity.ModifyDate;
                GrupoUsuarioToUpdate.ModifyUser = entity.ModifyUser;

                result = await base.Update(GrupoUsuarioToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["GrupoUsuario:error_update"], ex.ToString());
            }
            return result;
        }
    }
}
