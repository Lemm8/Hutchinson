using AuthApiExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApiExample.DTOs
{
    public class Auditorias6sDto
    {
        public string Nombre { get; set; }
        public int AreaId { get; set; }
        public int AuditorId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaTarget { get; set; }
        public DateTime? FechaCompleto { get; set; }
        public int Estado { get; set; }
        //public int AuditoriaId { get; set; }
        public int Mes { get; set; }
    }
}
