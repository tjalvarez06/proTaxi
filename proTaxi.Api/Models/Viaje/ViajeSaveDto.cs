namespace proTaxi.Api.Models.Viaje
{
    public record ViajeSaveDto
    {
        
        public DateTime? FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public int Calificacion { get; set; }
        public int TaxiId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreationUser { get; set; }
    }

}
