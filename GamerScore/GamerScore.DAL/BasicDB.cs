using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using MySqlConnector;

namespace GamerScore.DAL
{
    public class BasicDB : IBasicDB
    {
        private readonly string connectionString;
        public BasicDB(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public bool ConnectionTest()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageLogger.Log("Connection opened");
                return true;
            }
            catch (Exception e)
            {
                MessageLogger.Log("Exception caught testing connection: " + e.ToString());
                return false;
            }
            finally
            { 
                connection.Close();
                MessageLogger.Log("Connection closed");
            }
        }

        public bool ExecuteNonQuery(string _query)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageLogger.Log("Connection opened");
                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = _query;
                command.ExecuteNonQuery();

                return true;
            }
            catch(Exception e)
            {
                MessageLogger.Log($"Exception caught trying to execute query: {_query} Exception:" + e.ToString());
                return false;
            }
            finally 
            { 
                connection.Close();
                MessageLogger.Log("Connection closed");
            }
        }
    }
}
