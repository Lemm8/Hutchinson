using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiExample.Services
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public int accessTokenDurationInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string TestKey { get; set; }
    }
}
