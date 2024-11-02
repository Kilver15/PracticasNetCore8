namespace VentaAPI.Models
{
    public class DetalleVentaDTO
    {
        public long ProductoId { get; set; }
        public long VentaId { get; set; }
        public int Cantidad { get; set; }
    }
}
