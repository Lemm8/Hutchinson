using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApiExample.Models
{
    public partial class Auditorias6s
    {
        public string Nombre { get; set; }

        public int AuditorId { get; set; }

        public int? AreaId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaTarget { get; set; }
        public DateTime? FechaCompleto { get; set; }
        public int Estado { get; set; }
        public int? CalificacionId { get; set; }
        public int AuditoriaId { get; set; }
        public int mes { get; set; }

        public Areas6s Area { get; set; }
        public Auditores6s Auditor { get; set; }
        public Calificaciones6s Calificacion { get; set; }
    }
}
