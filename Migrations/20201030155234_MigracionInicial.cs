using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpleadoNomina.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCompleto = table.Column<string>(nullable: true),
                    Telefono = table.Column<long>(nullable: false),
                    Departamento = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EmpleadoId);
                });

            migrationBuilder.CreateTable(
                name: "Nominas",
                columns: table => new
                {
                    NominaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpleadoId = table.Column<int>(nullable: false),
                    SalarioMensual = table.Column<double>(nullable: false),
                    HorasExtra = table.Column<double>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    SFS = table.Column<double>(nullable: false),
                    AFP = table.Column<double>(nullable: false),
                    ISR = table.Column<double>(nullable: false),
                    SueldoTotal = table.Column<double>(nullable: false),
                    TotalDecuentos = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominas", x => x.NominaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Nominas");
        }
    }
}
