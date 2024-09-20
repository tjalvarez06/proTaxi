namespace proTaxi.Api.Models.Taxi
{
    public class TaxiUpdateDto
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyUser { get; set; }
    }
}
