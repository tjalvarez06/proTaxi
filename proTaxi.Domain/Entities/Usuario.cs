using proTaxi.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace proTaxi.Domain.Entities
{
    [Table("Usuario", Schema = "dbo")]
    public sealed class Usuario : AuditEntity<int>
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int GrupoUsuariosId { get; set; }
    }
}
