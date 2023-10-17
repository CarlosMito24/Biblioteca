using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Biblioteca.Modelos
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<VariablesUsuarios> Tabla_Usuarios { get; set; }
        public DbSet<VariablesLibro> Tabla_Libros { get; set; }
        public DbSet<VariablesRegistro> Tabla_Registros { get; set; }
    }
}
