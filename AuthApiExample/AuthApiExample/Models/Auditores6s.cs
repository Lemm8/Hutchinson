using System;
using System.Collections.Generic;

namespace AuthApiExample.Models
{
    public partial class Auditores6s
    {
        public Auditores6s()
        {
            Auditorias6s = new HashSet<Auditorias6s>();
        }

        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int UserId { get; set; }

        public ICollection<Auditorias6s> Auditorias6s { get; set; }
    }
}
