using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tabla_Libros",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabla_Libros", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tabla_Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teléfono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    DUI = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabla_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tabla_Registros",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariablesUsuariosID = table.Column<int>(type: "int", nullable: false),
                    VariablesLibroID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabla_Registros", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tabla_Registros_Tabla_Libros_VariablesLibroID",
                        column: x => x.VariablesLibroID,
                        principalTable: "Tabla_Libros",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tabla_Registros_Tabla_Usuarios_VariablesUsuariosID",
                        column: x => x.VariablesUsuariosID,
                        principalTable: "Tabla_Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tabla_Registros_VariablesLibroID",
                table: "Tabla_Registros",
                column: "VariablesLibroID");

            migrationBuilder.CreateIndex(
                name: "IX_Tabla_Registros_VariablesUsuariosID",
                table: "Tabla_Registros",
                column: "VariablesUsuariosID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tabla_Registros");

            migrationBuilder.DropTable(
                name: "Tabla_Libros");

            migrationBuilder.DropTable(
                name: "Tabla_Usuarios");
        }
    }
}
