using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;

namespace DatabaseInfo
{
    public class DBUtilities
    {
        public static SqlConnectionStringBuilder StringConnection(string server, string DB, string login, string ignoto)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder["Data Source"] = server;
            builder["Initial Catalog"] = DB;
            builder["User ID"] = login;
            builder["Password"] = ignoto;
            return builder;
        }

        private SqlConnection MadeConnection(SqlConnectionStringBuilder st)
        {
            var stringConnection = st.ToString();
            SqlConnection connection;
            try
            {
                 connection =  new SqlConnection(stringConnection);
                 connection.Open();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw;
            }
            
            return connection;
        }

        public List<string> ListDB(SqlConnectionStringBuilder st)
        {
            var connection = MadeConnection(st);

            var dbNames = new List<string>();
            //Definimos el commando de ListDB
            StringBuilder queryInfoDB = new StringBuilder();
            queryInfoDB.Append("SELECT name, CAST(create_date AS DATE) CREATION_DATE, state_desc, recovery_model_desc AS RecModel");
            queryInfoDB.Append(" FROM sys.databases;");

            //Creamos el SQLCommand
            SqlCommand sqlCommand = new SqlCommand(queryInfoDB.ToString(), connection);
            
            try
            {
                var dbValue = string.Empty;
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    foreach (DbDataRecord item in reader)
                    {
                        dbNames.Add($"{item["name"]} {item["CREATION_DATE"]}");
                    }
                }        
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw;
            }
            finally
            {
                connection.Dispose();
            }
            return dbNames; 
        }
    }
}
