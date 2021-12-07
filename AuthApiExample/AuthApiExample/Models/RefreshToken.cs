using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiExample.Models
{
    public class RefreshToken
    {
        public int ID { get; set; }
        public string NoEmp { get; set; }
        public string RefreshTokenSessionId { get; set; }
        public string Ip { get; set; }
        public string Source { get; set; }
        public int Revoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirityAt { get; set; }
    }
}
