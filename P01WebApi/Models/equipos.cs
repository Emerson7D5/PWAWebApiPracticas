using System.ComponentModel.DataAnnotations;

namespace P01WebApi.Models
{
    public class equipos
    {
        [Key]
        public int id_equipos { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int? tipo_equipo_id { get; set; }
        public int? marca_id { get; set; }
    }
}
