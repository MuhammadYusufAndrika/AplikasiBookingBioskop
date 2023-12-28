using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using TicketManagenement.Model.Entity;
using TicketManagenement.Model.Context;

namespace TicketManagenement.Model.Repository
{
    public class movieRepository
    {

        private MySqlConnection _cnn;
        private Movie _movie;

        public movieRepository(DbContext context)
        {
            // membnuka koneksi
            _cnn = (context.ConnectionOpen());
        }


        // method untuk membaca movie
        public List<Movie> readMovie()
        {
            List<Movie> list = new List<Movie>();

            string sql = "SELECT *FROM movie";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                using (MySqlDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        _movie = new Movie();
                        _movie.movieId = int.Parse(dtr["movie_Id"].ToString());
                        _movie.movieName = dtr["movieName"].ToString();
                        _movie.movieAmount = int.Parse(dtr["movieAmount"].ToString());
                        list.Add(_movie);
                    }
                }
            }

            return list;
        }

        // method menambahkan movie
        public int InsertMovie(Movie movie)
        {
            int result = 0;

            string sql = "INSERT INTO movie(movieName, movieAmount) VALUES (@movieName, @movieAmount)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@movieName", movie.movieName);
                cmd.Parameters.AddWithValue("@movieAmount", movie.movieAmount);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }

        // method untuk menghpaus movie
        public int delete(Movie movie)
        {
            int result = 0;

            string sql = "DELETE FROM movie WHERE movie_id = @movie_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@movie_id", movie.movieId);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Error: {0}", ex.Message);

                }
            }

            return result;
        }
    }
}
