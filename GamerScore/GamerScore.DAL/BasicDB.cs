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
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            { connection.Close(); }
        }

        public bool ExecuteNonQuery(string _query)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = _query;
                command.ExecuteNonQuery();

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally { connection.Close(); }
        }
    }
}
