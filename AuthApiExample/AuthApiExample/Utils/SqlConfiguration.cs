using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiExample.Utils
{
    public class SqlConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Database { get; set; }

        public SqlConfiguration()
        {
            Username = "auditorias";
            Password = "lemm2301";
            Address = "LAPTOP-39MVKQB3\\SQLEXPRESS";
            Database = "Hutchinson";

        }
    }
}
