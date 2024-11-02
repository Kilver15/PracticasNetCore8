namespace VentaAPI.Models
{
    public class VentaItem
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string MetodoPago { get; set; }
        public decimal Total { get; set; }
    }
}
