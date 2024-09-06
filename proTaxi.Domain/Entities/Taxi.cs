
using proTaxi.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace proTaxi.Domain.Entities
{
    [Table("Taxi", Schema = "dbo")]
    public sealed class Taxi : AuditEntity<int>
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }
        public string? Placa { get; set; }
    }
}
