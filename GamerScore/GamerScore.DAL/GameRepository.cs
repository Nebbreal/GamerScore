using Gamerscore.Core;
using Gamerscore.Core.Interfaces.Repositories;
using GamerScore.DTO;
using GamerScore.Exceptions;
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
                    MessageLogger.Log($"Exception caught trying to execute gameInfoQuery: {query} Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return games;
            }
        }

        public Game GetGameById(int _gameId)
        {
            int gameId = -1;
            string gameTitle = string.Empty;
            string gameDescription = string.Empty;
            string gameDeveloper = string.Empty;
            string gameThumbnailImageUrl = string.Empty;

            if (_gameId < 0)
            {
                throw new DataFetchFailedException("Id is negative");
            }

            using (MySqlConnection connection = new(connectionString))
            {
                //Get data from the "game" table
                string gameInfoQuery = "SELECT id, title, description, developer, thumbnailImageUrl FROM  game WHERE id = @gameId";
                
                Game game;
                try
                {
                    connection.Open();
                    MessageLogger.Log("Connection opened");

                    MySqlCommand command = new MySqlCommand(gameInfoQuery, connection);

                    command.Parameters.AddWithValue("@gameId", _gameId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gameId = reader.GetInt32("id");
                            gameTitle = reader.GetString("title");
                            gameDescription = reader.GetString("description");
                            gameDeveloper = reader.GetString("developer");
                            gameThumbnailImageUrl = reader.GetString("thumbnailImageUrl");
                            
                        }
                        MessageLogger.Log("Game table read");
                    }

                    string imagesQuery = "SELECT name, imageUrl FROM game_image WHERE game_id = @gameId;";
                    command = new MySqlCommand(imagesQuery, connection);
                    command.Parameters.AddWithValue("@gameId", _gameId);

                    List<GameImage> images = new List<GameImage>();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string gameImageName = reader.GetString("name");
                            string gameImageUrl = reader.GetString("imageUrl");

                            images.Add(new GameImage(gameImageName, gameImageUrl));
                        }
                    }
                    if (images.Count > 0)
                    {
                        game = new Game(gameId, gameTitle, gameDescription, gameDeveloper, gameThumbnailImageUrl, images);
                    }
                    else
                    {
                        game = new Game(gameId, gameTitle, gameDescription, gameDeveloper, gameThumbnailImageUrl);
                    }
                    
                }
                catch (Exception e)
                {
                    throw new DataFetchFailedException("GetGameById failed" , e);
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }

                return game;
            }   
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

                    //Link the game to the genres
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

                        MessageLogger.Log("ImageUrls linked");
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    MessageLogger.Log($"Exception caught trying to execute gameInfoQuery for CreateGame Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return false;
            }
        }

        public bool EditGame (int _gameId, string _title, string _description, string _developer, string _thumbnailImageUrl, List<string> _imageUrls, List<int> _genreIds)
        {
            using (MySqlConnection connection = new(connectionString))
            {
                connection.Open();
                MessageLogger.Log("Connection opened");

                MySqlTransaction transaction = connection.BeginTransaction();
                string query = "";
                try
                {
                    //Create game in "game" table
                    query = "UPDATE game SET title = @title, description = @description, developer = @developer, thumbnailImageUrl = @thumbnailImageUrl WHERE id = @gameId";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Parameters.AddWithValue("@title", _title);
                    command.Parameters.AddWithValue("@description", _description);
                    command.Parameters.AddWithValue("@developer", _developer);
                    command.Parameters.AddWithValue("@thumbnailImageUrl", _thumbnailImageUrl);

                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    //Clear the linked genres
                    query = "DELETE FROM game_genre WHERE game_id = @gameId";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    MessageLogger.Log("Genres cleared");

                    //Link the game to the genres
                    //Concatinate the values string
                    string values = string.Empty;
                    int lastGenreId = _genreIds.Last();
                    foreach (int genreId in _genreIds)
                    {
                        if (genreId != lastGenreId)
                        {
                            values += $"({_gameId}, {genreId}),";
                        }
                        else
                        {
                            values += $"({_gameId}, {genreId});";
                        }

                    }
                    query = "INSERT INTO game_genre (game_id, genre_id) VALUES " + values;

                    command = new MySqlCommand(query, connection);
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    MessageLogger.Log("Genres linked");

                    //Clear linked images
                    query = "DELETE FROM game_image WHERE game_id = @gameId";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    MessageLogger.Log("Images cleared");

                    //Image linking
                    if (_imageUrls != null && _imageUrls.Count > 0)
                    {
                        values = string.Empty;
                        string lastImageUrl = _imageUrls.Last();
                        foreach (string imageUrl in _imageUrls)
                        {
                            if (imageUrl != lastImageUrl)
                            {
                                values += $"({_gameId}, '{_title}_image', '{imageUrl}'),";
                            }
                            else
                            {
                                values += $"({_gameId}, '{_title}_image', '{imageUrl}');";
                            }

                        }

                        query = "INSERT INTO game_image (game_id, name, imageUrl) VALUES " + values;

                        command = new MySqlCommand(query, connection);
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();

                        MessageLogger.Log("ImageUrls linked");
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    MessageLogger.Log($"Exception caught trying to execute {query} for EditGame Exception:" + e.ToString());
                }
                finally
                {
                    connection.Close();
                    MessageLogger.Log("Connection closed");
                }
                return false;
            }
        }
    
        public bool DeleteGame(int _gameId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MessageLogger.Log("Connection opened");

                MySqlTransaction transaction = connection.BeginTransaction();
                string query = "";

                try
                {
                    //Clear the linked genres
                    query = "DELETE FROM game_genre WHERE game_id = @gameId";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    MessageLogger.Log("Genres cleared");

                    //Clear the linked images
                    query = "DELETE FROM game_image WHERE game_id = @gameId";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    MessageLogger.Log("Images cleared");

                    //Delete the linked reviews
                    query = "DELETE FROM review WHERE game_id = @gameId";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    MessageLogger.Log("Reviews cleared");

                    //Delete the game
                    query = "DELETE FROM game WHERE id = @gameId";

                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@gameId", _gameId);
                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    MessageLogger.Log("Reviews cleared");

                    transaction.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    MessageLogger.Log($"Exception caught trying to execute {query} for EditGame Exception:" + e.ToString());
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
}
