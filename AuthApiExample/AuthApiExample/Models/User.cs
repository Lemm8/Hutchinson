using AuthApiExample.Models;
using AuthApiExample.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AuthApiExample.Models
{
    public class User
    {
        public string NoEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public byte[] Foto { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
        public string Planta { get; set; }
        public string Perfil { get; set; }
        public List<Permission> Permissions { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public int Leer(string Username, string Password)
        {
            var sql = SqlManager.Instance;

            try
            {
                string query = "select u.NoEmpleado, u.Nombre, u.ApellidoPaterno, u.ApellidoMaterno, u.Foto, u.Correo, u.Departamento, u.Planta, u.Usuario, u.Contrasena, p.nombrePerfil as Perfil from [dbo].[User] u inner join Perfil p on p.perfil_id = u.Perfil WHERE u.Usuario = @username and u.Contrasena = @password";

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@username", Username);
                parameters.Add("@password", Password);

                DataSet ds = sql.Select(query, parameters);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                    return 101;
                this.NoEmpleado = dt.Rows[0]["NoEmpleado"].ToString();
                this.Nombre = dt.Rows[0]["Nombre"].ToString();
                this.ApellidoPaterno = dt.Rows[0]["ApellidoPaterno"].ToString();
                this.ApellidoMaterno = dt.Rows[0]["ApellidoMaterno"].ToString();
                //this.Foto = (byte[])dt.Rows[0]["Foto"];
                this.Correo = dt.Rows[0]["Correo"].ToString();
                this.Perfil = dt.Rows[0]["Perfil"].ToString();
                this.Departamento = dt.Rows[0]["Departamento"].ToString();
                this.Usuario = dt.Rows[0]["Departamento"].ToString();
                this.Contrasena = dt.Rows[0]["Departamento"].ToString();
                //this.Permissions = dt.Rows[0]["Permissions"].ToString();
            }
            catch (SqlException e)
            {
                throw e;
            }
            return 0;
        }
    }
}
