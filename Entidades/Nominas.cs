using System;
//Using agregados
using System.ComponentModel.DataAnnotations;

namespace EmpleadoNomina.Entidades
{
    public class Nominas
    {
        [Key]
        public int NominaId { get; set; }
        public int EmpleadoId { get; set; }
        public double SalarioMensual { get; set; }
        public double HorasExtra { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double SFS { get; set; }
        public double AFP { get; set; }
        public double ISR { get; set; }
        public double SueldoTotal { get; set; }
        public double TotalDecuentos { get; set; }
    }
}