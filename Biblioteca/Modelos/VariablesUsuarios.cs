using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modelos
{
    public class VariablesUsuarios
    {
        [Key]
        public int ID { get; set; }

        [Required (ErrorMessage = "El campo Nombre es requerido para guardar el usuario")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es requerido para guardar el usuario")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es requerido para guardar el usuario")]
        [MaxLength(9)]
        [MinLength(9)]
        public string Teléfono { get; set; }

        [Required(ErrorMessage = "El campo DUI es requerido para guardar el usuario")]
        [MaxLength(10)]
        [MinLength(10)]
        public string DUI { get; set; }

        public ICollection<VariablesRegistro> CrearRegistro { get; set; }
    }
}