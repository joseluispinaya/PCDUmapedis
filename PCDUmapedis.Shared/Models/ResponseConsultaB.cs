using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCDUmapedis.Shared.Models
{
    public class ResponseConsultaB
    {
        public int Idbono { get; set; }
        public int Idmes { get; set; }
        public string? DescripcionMes { get; set; }
        public float Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? FechaRegistro { get; set; }
        public string? NomAdmin { get; set; }
        public bool Estado { get; set; }

        public string MontoCadena => $"Bs/ {Monto:F2}";
        public string Atendio => $"Cajero: {NomAdmin}";
    }
}
