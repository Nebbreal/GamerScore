using Gamerscore.Core;
using Gamerscore.Core.Interfaces;
using Gamerscore.DTO;
using GamerScore.DTO;
using MySqlConnector;

namespace GamerScore.DAL
{
    public class GameRepository : IGameRepository
    {
        private readonly string connectionString;
        public GameRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public bool CreateGame(string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<int> _genreIds)
        {
            using (MySqlConnection connection = new(connectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                string query = "";
                try
                {
                    MessageLogger.Log("Connection opened");

                    //Create game in "game" table
                    query = "INSERT INTO game (title, description, developer, thumbnailImageUrl) VALUES (@title, @description, @developer, @thumbnailImageUrl); SELECT LAST_INSERT_ID() AS lastInsertId;";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@title", _title);
                    command.Parameters.AddWithValue("@description", _description);
                    command.Parameters.AddWithValue("@developer", _developer);
                    command.Parameters.AddWithValue("@thumbnailImageUrl", _thumbnailImageUrl);

                    command.Transaction = transaction;

                    int gameId = -1;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameId = int.Parse(reader["lastInsertId"].ToString());
                        }
                        if (gameId == -1)
                        {
                            throw new Exception("GameId reading failed");
                        }
                        MessageLogger.Log("GameId read");
                    }
                    
                    //Link the game to the games
                    //Concatinate the values string

                    string values = string.Empty;
                    int lastGenreId = _genreIds.Last();
                    foreach (int genreId in _genreIds)
                    {
                        if (genreId != lastGenreId)
                        {
                            values += $"({gameId}, {genreId}),";
                        }
                        else
                        {
                            values += $"({gameId}, {genreId});";
                        }
                        
                    }
                    query = "INSERT INTO game_genre (game_id, genre_id) VALUES " + values;

                    command = new MySqlCommand(query, connection);
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    MessageLogger.Log("Genres linked");

                    //Image linking
                    if (_imageUrls != null && _imageUrls.Count > 0)
                    {
                        values = string.Empty;
                        string lastImageUrl = _imageUrls.Last();
                        foreach (string imageUrl in _imageUrls)
                        {
                            if (imageUrl != lastImageUrl)
                            {
                                values += $"({gameId}, '{_title}_image', '{imageUrl}'),";
                            }
                            else
                            {
                                values += $"({gameId}, '{_title}_image', '{imageUrl}');";
                            }

                        }

                        query = "INSERT INTO game_image (game_id, name, imageUrl) VALUES " + values;

                        command = new MySqlCommand(query, connection);
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();

                        MessageLogger.Log("Images linked");
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    MessageLogger.Log($"Exception caught trying to execute query fore CreateGame Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return false;
            }
        }

        public List<Game> GetAllGames()
        {
            string query = "SELECT id, title, description, developer, thumbnailImageUrl FROM game ORDER BY id DESC";
            List<Game> games = new List<Game>();

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
                        int gameId = int.Parse(reader["id"].ToString());
                        string? gameTitle = reader["title"].ToString() ?? null;
                        string? gameDescription = reader["description"].ToString() ?? null;
                        string? gameDeveloper = reader["developer"].ToString() ?? null;
                        string? gameThumbnailImageUrl = reader["thumbnailImageUrl"].ToString() ?? null;
                        Game game = new(gameId, gameTitle, gameDescription, gameDeveloper, gameThumbnailImageUrl);

                        games.Add(game);
                    }
                    return games;
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
                return games;
            }
        }
    }
}
