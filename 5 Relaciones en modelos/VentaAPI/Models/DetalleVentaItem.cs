namespace VentaAPI.Models
{
    public class DetalleVentaItem
    {
        public long Id { get; set; }
        public long ProductoId { get; set; }
        public long VentaId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
    }
}
