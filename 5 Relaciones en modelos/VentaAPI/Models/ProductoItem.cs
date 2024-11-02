namespace VentaAPI.Models
{
    public class ProductoItem
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public long CategoriaId { get; set; }
        public int Stock { get; set; }

    }
}
