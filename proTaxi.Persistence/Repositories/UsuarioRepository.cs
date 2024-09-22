

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using proTaxi.Domain.Entities;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Context;
using proTaxi.Persistence.Exceptions;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Usuario;
using proTaxi.Persistence.Repository;

namespace proTaxi.Persistence.Repositories
{
    public sealed class UsuarioRepository : BaseRepository<Usuario, int>, IUsuarioRepository
    {
        private readonly TaxiDb taxiDb;
        private readonly ILogger<UsuarioRepository> logger;
        private readonly IConfiguration configuration;

        public UsuarioRepository(TaxiDb taxiDb,
                               ILogger<UsuarioRepository> logger,
                               IConfiguration configuration) : base(taxiDb)
        {
            this.taxiDb = taxiDb;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<DataResult<List<UsuarioModel>>> GetUsuarios()
        {
            DataResult<List<UsuarioModel>> result = new DataResult<List<UsuarioModel>>();
            try
            {
                var usuarios = await (from usuario in this.taxiDb.Usuario
                                    where usuario.Deleted == false
                                    select new UsuarioModel
                                    {
                                        Id = usuario.Id,
                                        Documento = usuario.Documento,
                                        Nombre = usuario.Nombre,
                                        Apellido = usuario.Apellido,
                                        GrupoUsuarioId = usuario.GrupoUsuarioId
                                    }).ToListAsync();

                result.Result = usuarios;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = this.configuration["usuario: error_get_usuario"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<DataResult<UsuarioModel>> GetUsuarios(string nombre)
        {
            DataResult<UsuarioModel> result = new DataResult<UsuarioModel>();
            try
            {
                var usuario = await this.taxiDb.Usuario
                                             .SingleOrDefaultAsync(usuario => usuario.Nombre == nombre
                                                                   && usuario.Deleted == false);

                if (nombre == null)
                {
                    result.Message = "El usuario no se encuentra registrado o fue eliminado";
                    result.Success = false;
                }
                result.Result = new UsuarioModel()
                {
                    Id = usuario.Id,
                    Documento = usuario.Documento,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    GrupoUsuarioId = usuario.GrupoUsuarioId
                };
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = this.configuration["usuario:error_get_usuario_nombre"];
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public override async Task<bool> Save(Usuario entity)
        {
            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["usuario:entity"]);
                }

                if (await base.Exists(usuario => usuario.Nombre == entity.Nombre))
                    throw new UsuarioDataException(this.configuration["usuario:nombre_exists"]);

                if (string.IsNullOrEmpty(entity.Nombre))
                {
                    throw new UsuarioDataException(this.configuration["usuario:nombre_is_null"]);
                }
                if (entity.Nombre.Length > 50)
                {
                    throw new UsuarioDataException(this.configuration["usuario:nombre_length"]);
                }
                

                result = await base.Save(entity);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["usuario:error_save"], ex.ToString());
            }
            return result;
        }
        public override async Task<bool> Update(Usuario entity)
        {

            bool result = false;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(this.configuration["usuario:entity"]);
                }

                if (string.IsNullOrEmpty(entity.Nombre))
                {
                    throw new UsuarioDataException(this.configuration["usuario:nombre_is_null"]);
                }
                if (entity.Nombre.Length > 50)
                {
                    throw new UsuarioDataException(this.configuration["usuario:nombre_length"]);
                }
                Usuario? usuarioToUpdate = this.taxiDb.Usuario.Find(entity.Id);

                    usuarioToUpdate.Id = entity.Id;
                    usuarioToUpdate.Documento = entity.Documento;
                    usuarioToUpdate.Nombre = entity.Nombre;
                    usuarioToUpdate.Apellido = entity.Apellido;
                    usuarioToUpdate.GrupoUsuarioId = entity.GrupoUsuarioId;
                    usuarioToUpdate.ModifyDate = entity.ModifyDate;
                    usuarioToUpdate.ModifyUser = entity.ModifyUser;
                

                result = await base.Update(usuarioToUpdate);
            }
            catch (Exception ex)
            {
                result = false;
                this.logger.LogError(this.configuration["usuario:error_update"], ex.ToString());
            }
            return result;
        }
    }
}
