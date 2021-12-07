using System;
using System.Collections.Generic;

namespace AuthApiExample.Models
{
    public partial class Calificaciones6s
    {
        public Calificaciones6s()
        {
            Auditorias6s = new HashSet<Auditorias6s>();
        }

        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }
        public double S5 { get; set; }
        public double S6 { get; set; }
        public int CalificacionId { get; set; }

        public ICollection<Auditorias6s> Auditorias6s { get; set; }
    }
}
