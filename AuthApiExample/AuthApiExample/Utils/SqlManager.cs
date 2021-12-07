using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthApiExample.Utils
{
    public class SqlManager
    {
        public string connString { get; set; }
        public SqlConfiguration Config { get; set; }
        public string ErrorMsg { get; set; }

        private static volatile SqlManager instance;
        private static object syncRoot = new Object();

        private SqlManager()
        {
            Config = new SqlConfiguration();
        }
        public static SqlManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SqlManager();
                    }
                }

                return instance;
            }
        }

        public SqlManager UseConfiguration(SqlConfiguration config)
        {
            Config = config;

            return this;
        }


        private SqlParameter[] GetParameters(Dictionary<string, object> parameters)
        {

            SqlParameter[] ps = parameters.AsEnumerable().Select(c => new SqlParameter()
            {
                ParameterName = c.Key,
                Value = c.Value

            }).ToArray();

            return ps;

        }

        public DataSet Select(string query, Dictionary<string, object> parameters = null, bool isStoreProcedure = false)
        {
            SqlConnection conn = ConfigureConnection();
            conn.Open();
            try
            {

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.CommandType = (isStoreProcedure == true ? CommandType.StoredProcedure : CommandType.Text);
                    da.SelectCommand.CommandTimeout = 0;

                    if (parameters != null)
                    {
                        da.SelectCommand.Parameters.AddRange(GetParameters(parameters));
                    }

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    return ds;
                }
            }
            catch (SqlException e)
            {
                ErrorMsg = e.Message;
                return null;
            }

        }

        public int Insert(string query, Dictionary<string, object> parameters, bool isStoreProcedure = false)
        {
            SqlConnection conn = ConfigureConnection();
            query += "; SELECT SCOPE_IDENTITY()";

            using (SqlCommand comm = new SqlCommand(query, conn))
            {
                try
                {

                    comm.Connection.Open();

                    comm.CommandType = (isStoreProcedure == true ? CommandType.StoredProcedure : CommandType.Text);

                    if (parameters != null)
                    {
                        comm.Parameters.AddRange(GetParameters(parameters));
                    }

                    int newID = Convert.ToInt32(comm.ExecuteScalar());

                    comm.Connection.Close();

                    return newID;

                }
                catch (SqlException e)
                {
                    comm.Connection.Close();
                    ErrorMsg = e.Message;

                    return 0;
                }

            }

        }

        public int Update(string query, Dictionary<string, object> parameters, bool isStoreProcedure = false)
        {

            SqlConnection conn = ConfigureConnection();
            query += ";SELECT @@ROWCOUNT";

            using (SqlCommand comm = new SqlCommand(query, conn))
            {
                try
                {
                    if (comm.Connection.State == ConnectionState.Open)
                    {
                        comm.Connection.Close();
                    }

                    comm.Connection.Open();

                    comm.CommandType = (isStoreProcedure == true ? CommandType.StoredProcedure : CommandType.Text);

                    if (parameters != null)
                    {

                        comm.Parameters.AddRange(GetParameters(parameters));
                    }

                    int rows = Convert.ToInt32(comm.ExecuteScalar());

                    comm.Connection.Close();

                    return rows;

                }
                catch (SqlException e)
                {
                    comm.Connection.Close();
                    ErrorMsg = e.Message;

                    return 0;
                }

            }
        }

        public int Delete(string query, Dictionary<string, object> parameters, bool isStoreProcedure = false)
        {
            SqlConnection conn = ConfigureConnection();
            query += ";SELECT @@ROWCOUNT";

            using (SqlCommand comm = new SqlCommand(query, conn))
            {
                try
                {

                    if (comm.Connection.State == ConnectionState.Open)
                    {
                        comm.Connection.Close();
                    }

                    comm.Connection.Open();

                    comm.CommandType = (isStoreProcedure == true ? CommandType.StoredProcedure : CommandType.Text);

                    if (parameters != null)
                    {
                        comm.Parameters.AddRange(GetParameters(parameters));
                    }

                    int rows = Convert.ToInt32(comm.ExecuteScalar());

                    comm.Connection.Close();

                    return rows;
                }
                catch (SqlException e)
                {
                    comm.Connection.Close();
                    ErrorMsg = e.Message;

                    return 0;
                }

            }

        }

        private SqlConnection ConfigureConnection()
        {
            string connString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", Config.Address, Config.Database, Config.Username, Config.Password);
            return new SqlConnection(connString);

        }
    }
}
