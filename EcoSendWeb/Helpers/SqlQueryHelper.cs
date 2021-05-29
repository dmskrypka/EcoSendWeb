using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace EcoSendWeb.Helpers
{
    public class SqlQueryHelper
    {
        public static DataTable ExecuteSqlQuery(DbConnection connection, string query)
        {
            SqlCommand cmd = new SqlCommand(query, connection as SqlConnection);

            if ((connection.State == ConnectionState.Closed))
            {
                connection.Open();
            }

            DataTable retVal = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(retVal);
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL Command failed", ex);
            }

            return retVal;
        }
    }
}