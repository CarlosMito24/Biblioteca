using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modelos
{
    public class VariablesLibro
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido para guardar el libro")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Autor es requerido para guardar el libro")]
        public string Autor { get; set; }

        public ICollection<VariablesRegistro> CrearRegistro { get; set; }
    }
}
