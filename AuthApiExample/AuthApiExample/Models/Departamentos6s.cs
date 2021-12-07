using System;
using System.Collections.Generic;

namespace AuthApiExample.Models
{
    public partial class Departamentos6s
    {
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; }
        public int? Aforo { get; set; }
        public int AreaId { get; set; }

        public Areas6s Area { get; set; }
    }
}
