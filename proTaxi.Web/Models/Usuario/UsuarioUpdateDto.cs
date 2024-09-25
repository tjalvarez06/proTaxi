namespace proTaxi.Web.Models.Usuario
{
    public class UsuarioUpdateDto :UsuarioBaseDto
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        
    }
}