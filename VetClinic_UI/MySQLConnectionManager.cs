using System.Configuration;
using MySql.Data.MySqlClient;

namespace VetClinic_UI
{
    public class MySQLConnectionManager
    {
        // Connection string name
        private string connectionStringName;

        // MySQL connection object
        private MySqlConnection connection;

        // Constructor
        public MySQLConnectionManager(string connectionStringName)
        {
            this.connectionStringName = connectionStringName;
            // Retrieve connection string from configuration
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            // Initialize connection
            connection = new MySqlConnection(connectionString);
        }

        // Open connection
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Close connection
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // Get connection
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}