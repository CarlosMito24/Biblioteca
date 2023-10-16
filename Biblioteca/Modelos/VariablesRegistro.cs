using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modelos
{
    public class VariablesRegistro
    {
        public int ID { get; set; }

        public int VariablesUsuariosID { get; set; }

        public int VariablesLibroID { get; set; }

        public VariablesUsuarios VariablesUsuarios { get; set; }

        public VariablesLibro VariablesLibro { get; set; }
    }
}
