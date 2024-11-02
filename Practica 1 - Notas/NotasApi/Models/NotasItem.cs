namespace NotasApi.Models
{
    public class NotasItem
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Detalle { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
