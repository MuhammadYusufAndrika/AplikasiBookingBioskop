using System;
using System.Collections.Generic;

using System.Windows.Forms;
using TicketManagenement.Model.Context;
using TicketManagenement.Model.Entity;
using TicketManagenement.Model.Repository;

namespace TicketManagenement.Controller
{
    public class movieController
    {
        private movieRepository _repository;

        //menampilkan daftar film
        public List<Movie> GetMovies() 
        {
            List<Movie> list = new List<Movie>();

            using (DbContext context = new DbContext())
            {
                _repository = new movieRepository(context);
                list = _repository.readMovie();
            }

            return list;
        }

        // menambahkan data film
        public int addMovie(Movie movie)
        {
            int result = 0;
            if (string.IsNullOrEmpty(movie.movieName))
            {
                MessageBox.Show("Nama Film blm ada !!!", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            if (string.IsNullOrEmpty(movie.movieAmount.ToString()))
            {
                MessageBox.Show("Harga Film blm ada !!!", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new movieRepository(context);
                result = _repository.InsertMovie(movie);
            }

            return result;
        }

        // menghapus data film
        public int movieDelete(Movie movie)
        {
            int result = 0;

            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menghapus data ini? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (konfirmasi == DialogResult.Yes)
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new movieRepository(context);
                    result = _repository.delete(movie);

                }

                if (result > 0)
                {
                    MessageBox.Show("Data berhasil dihapus", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return result;
        }
    }
}
