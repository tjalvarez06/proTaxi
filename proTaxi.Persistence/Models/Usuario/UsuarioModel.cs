
namespace proTaxi.Persistence.Models.Usuario
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int GrupoUsuariosId { get; set; }
    }
}
