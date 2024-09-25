namespace proTaxi.Web.Models.Usuario
{
    public class UsuarioSaveDto :UsuarioBaseDto
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int GrupoUsuariosId { get; set; }
    }
}
