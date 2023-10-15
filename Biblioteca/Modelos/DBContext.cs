using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Modelos
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<VariablesUsuarios> Tabla_Usuarios { get; set; }
        public DbSet<VariablesLibro> Tabla_Libros { get; set; }
    }
}
