using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modelos
{
    public class VariablesUsuarios
    {
        [Key]
        public int ID { get; set; }

        [Required (ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es requerido")]
        public string Apellido { get; set; }

        [Required (ErrorMessage = "El campo Teléfono es requerido")]
        public string Teléfono { get; set; }

        [Required(ErrorMessage = "El campo DUI es requerido")]
        [MaxLength(9)]
        [MinLength(9)]
        public int DUI { get; set; }
    }
}