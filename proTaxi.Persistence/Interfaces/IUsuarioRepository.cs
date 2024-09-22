using proTaxi.Domain.Entities;
using proTaxi.Domain.Interfaces;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Models.Usuario;
using proTaxi.Persistence.Models.Viaje;


namespace proTaxi.Persistence.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, int>
    {
        Task<DataResult<List<UsuarioModel>>> GetUsuarios();
        Task<DataResult<UsuarioModel>> GetUsuarios(string nombre);
    }
}
