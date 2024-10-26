using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colegio.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tab_Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescGenero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Seccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescSeccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Seccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Alumno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    FechaNac = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Alumno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tab_Alumno_Tab_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Tab_Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Profesor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Profesor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tab_Profesor_Tab_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Tab_Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Grado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProfesor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Grado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tab_Grado_Tab_Profesor_IdProfesor",
                        column: x => x.IdProfesor,
                        principalTable: "Tab_Profesor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tab_AlumnoGrado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlumno = table.Column<int>(type: "int", nullable: false),
                    IdGrado = table.Column<int>(type: "int", nullable: false),
                    IdSeccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_AlumnoGrado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tab_AlumnoGrado_Tab_Alumno_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "Tab_Alumno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tab_AlumnoGrado_Tab_Grado_IdGrado",
                        column: x => x.IdGrado,
                        principalTable: "Tab_Grado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tab_AlumnoGrado_Tab_Seccion_IdSeccion",
                        column: x => x.IdSeccion,
                        principalTable: "Tab_Seccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tab_Alumno_IdGenero",
                table: "Tab_Alumno",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_AlumnoGrado_IdAlumno",
                table: "Tab_AlumnoGrado",
                column: "IdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_AlumnoGrado_IdGrado",
                table: "Tab_AlumnoGrado",
                column: "IdGrado");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_AlumnoGrado_IdSeccion",
                table: "Tab_AlumnoGrado",
                column: "IdSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_Grado_IdProfesor",
                table: "Tab_Grado",
                column: "IdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_Profesor_IdGenero",
                table: "Tab_Profesor",
                column: "IdGenero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tab_AlumnoGrado");

            migrationBuilder.DropTable(
                name: "Tab_Alumno");

            migrationBuilder.DropTable(
                name: "Tab_Grado");

            migrationBuilder.DropTable(
                name: "Tab_Seccion");

            migrationBuilder.DropTable(
                name: "Tab_Profesor");

            migrationBuilder.DropTable(
                name: "Tab_Genero");
        }
    }
}
