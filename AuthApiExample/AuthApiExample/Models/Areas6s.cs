using System;
using System.Collections.Generic;

namespace AuthApiExample.Models
{
    public partial class Areas6s
    {
        public Areas6s()
        {
            Auditorias6s = new HashSet<Auditorias6s>();
            Departamentos6s = new HashSet<Departamentos6s>();
        }

        public int AreaId { get; set; }
        public string Nombre { get; set; }
        public int? AuditoriaId { get; set; }

        public ICollection<Auditorias6s> Auditorias6s { get; set; }
        public ICollection<Departamentos6s> Departamentos6s { get; set; }
    }
}
