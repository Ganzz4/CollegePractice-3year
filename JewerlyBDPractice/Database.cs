using System;
using System.Data.SqlClient;

namespace Lab11
{
    public class Database : IDisposable
    {
        private SqlConnection sqlConnection;

        public Database()
        {
            sqlConnection = new SqlConnection(@"Data Source=SANCHO;Initial Catalog=JEWERLYBD;Integrated Security=True;Encrypt=False;");
        }

        public void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

        public void Dispose()
        {
            if (sqlConnection != null)
            {
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }
    }
}
