

namespace proTaxi.Persistence.Models.Viaje
{
    public class ViajeModel
    {
        public int Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Desde { get; set; }
        public string? Hasta { get; set; }
        public int Calificacion { get; set; }
    }
}
