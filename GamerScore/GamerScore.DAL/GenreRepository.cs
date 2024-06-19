using Gamerscore.Core;
using Gamerscore.Core.Interfaces.Repositories;
using Gamerscore.DTO;
using MySqlConnector;

namespace GamerScore.DAL
{
    public class GenreRepository : IGenreRepository
    {
        private readonly string connectionString;
        public GenreRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public List<Genre> GetAllGenres()
        {
            string query = "SELECT id, name, imageUrl FROM genre";
            List<Genre> genres = new List<Genre>();

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    using MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int? genreID = reader["id"] == DBNull.Value ? null : int.Parse(reader["id"].ToString());
                        string? genreName = reader["name"].ToString() ?? null;
                        string? genreImageUrl = reader["imageUrl"].ToString() ?? null;
                        Genre genre = new(genreID, genreName, genreImageUrl);

                        genres.Add(genre);
                    }
                    return genres;
                }
                catch (Exception e)
                {
                    MessageLogger.Log($"Exception caught trying to execute query: {query} Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return genres;
            }
        }

        public Genre GetGenreByName(string _name)
        {
            string query = "SELECT id, name, imageUrl FROM genre WHERE name = @name";

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    using MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    command.Parameters.AddWithValue("@name", _name);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int? genreID = reader["id"] == DBNull.Value ? null : int.Parse(reader["id"].ToString());
                        string? genreName = reader["name"].ToString() ?? null;
                        string? genreImageUrl = reader["imageUrl"].ToString() ?? null;
                        Genre genre = new(genreID, genreName, genreImageUrl);

                        return genre;
                    }
                }
                catch (Exception e)
                {
                    MessageLogger.Log($"Exception caught trying to execute query: {query} Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return new Genre(null, null, null);
            }
        }

        public bool CreateGenre(string _name, string? _imageUrl)
        {
            string query = "INSERT INTO genre (name, imageUrl) VALUES (@name, @imageUrl);";
            if (_imageUrl == null)
            {
                query = "INSERT INTO genre (name) VALUES (@name);";
            }
            

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    using MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    command.Parameters.AddWithValue("@name", _name);
                    command.Parameters.AddWithValue("@imageUrl", _imageUrl);

                    command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    MessageLogger.Log($"Exception caught trying to execute query: {query} Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return false;
            }
        }

        public bool EditGenre(int _genreId, string _name, string? _imageUrl)
        {
            string query = "UPDATE genre SET name = @name, imageUrl = @imageUrl WHERE id = @genreId";

            using(MySqlConnection connection = new(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    command.Parameters.AddWithValue("@genreId", _genreId);
                    command.Parameters.AddWithValue("@name", _name);
                    command.Parameters.AddWithValue("@imageUrl", _imageUrl ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    MessageLogger.Log($"Exception caught trying to execute query: {query} Exception:" + e.ToString());
                }
                finally 
                { 
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
            }

            return false;
        }

        public bool DeleteGenre(int _genreId)
        {
            string query = "DELETE FROM genre WHERE id = @genreId";

            using (MySqlConnection connection = new(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    command.Parameters.AddWithValue("@genreId", _genreId);

                    command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    MessageLogger.Log($"Exception caught trying to execute query: {query} Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
            }

            return false;
        }
    }
}
