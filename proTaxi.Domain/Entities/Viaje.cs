using proTaxi.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace proTaxi.Domain.Entities
{
    [Table("Viaje", Schema = "dbo")]
    public sealed class Viaje : AuditEntity<int>
    {
        [Key]
        [Column("Id")]
        public override int Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Desde { get; set; }
        public string? Hasta { get; set; }
        public int Calificacion { get; set; }

    }
}
