using proTaxi.Domain.Entities;
using proTaxi.Domain.Interfaces;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Models.GrupoUsuarios;
using proTaxi.Persistence.Models.Usuario;

namespace proTaxi.Persistence.Interfaces
{
    public interface IGrupoUsuariosRepository : IRepository<GrupoUsuarios, int>
    {
        Task<DataResult<List<GrupoUsuariosModel>>> GetGruposUsuarios();
        Task<DataResult<GrupoUsuariosModel>> GetGrupoUsuarioById(int id);
    }
}
