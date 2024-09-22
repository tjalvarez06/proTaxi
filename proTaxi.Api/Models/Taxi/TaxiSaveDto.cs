namespace proTaxi.Api.Models.Taxi
{
    public record TaxiSaveDto
    {
        public string Placa { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreationUser { get; set; }
    }
}
