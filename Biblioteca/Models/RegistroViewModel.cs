using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido.. ")]
        [EmailAddress(ErrorMessage = "El campo {0} debe de ser un Correo eletrónico valido.. ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.. ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
