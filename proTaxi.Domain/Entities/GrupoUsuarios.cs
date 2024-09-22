

using proTaxi.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proTaxi.Domain.Entities
{
    [Table("GrupoUsuarios", Schema = "dbo")]
    public sealed class GrupoUsuarios : AuditEntity<int>
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }
        public string? Name { get; set; }
    }
}
