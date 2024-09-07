

namespace proTaxi.Domain.Models
{
    public class DataResult<TData>
    {
        public DataResult()
        {
            this.Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public TData Result { get; set; }
    }
}
