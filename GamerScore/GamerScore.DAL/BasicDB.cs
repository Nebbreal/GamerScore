using MySqlConnector;

namespace GamerScore.DAL
{
    public class BasicDB
    {
        private readonly string connectionString;
        public BasicDB(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public void ConnectionTest()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            connection.Close();
        }

        public void ExecuteNonQuery(string _query)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = _query;
            
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
