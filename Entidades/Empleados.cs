using System;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace EmpleadoNomina.Entidades
{
    public class Empleados
    {
        [Key]
        public int EmpleadoId { get; set; }
        public string NombreCompleto { get; set; }
        public long Telefono { get; set; }
        public string Departamento { get; set; }
        public string Puesto { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}