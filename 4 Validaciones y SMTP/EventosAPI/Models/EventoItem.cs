using System.ComponentModel.DataAnnotations;

namespace EventosAPI.Models
{
    public class EventoItem
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Detalle { get; set; }
        [DataType(DataType.Date)]
        public string Fecha { get; set; }

        public string Hora { get; set; }

        [EmailAddress]
        public string Creador { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;


    }
}
