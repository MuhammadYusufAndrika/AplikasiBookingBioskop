using System;
using System.Collections.Generic;

using System.Windows.Forms;
using TicketManagenement.Controller;
using TicketManagenement.Model.Entity;

namespace TicketManagenement.View
{
    public partial class manageSystem : Form
    {
        // movie
        private movieController _movieController;
        public List<Movie> listMovie = new List<Movie>();

        //sanck
        private snackController _snackController;
        public List<Snack> listSnack = new List<Snack>();

        public manageSystem()
        {
            _movieController = new movieController();
            _snackController = new snackController();

            InitializeComponent();
            InisialisasiMovie();
            InisialisasiSanck();

            loadDataMovie();
            loadDataSnack();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void InisialisasiMovie()
        {
            lvwMovie.View = System.Windows.Forms.View.Details;
            lvwMovie.FullRowSelect = true;
            lvwMovie.GridLines = true;

            lvwMovie.Columns.Add("No", 50, HorizontalAlignment.Left);
            lvwMovie.Columns.Add("ID Movie", 130, HorizontalAlignment.Center);
            lvwMovie.Columns.Add("Movie Name", 200, HorizontalAlignment.Center);
            lvwMovie.Columns.Add("Movie Amount", 150, HorizontalAlignment.Center);
        }

        private void InisialisasiSanck()
        {
            lvwSnack.View = System.Windows.Forms.View.Details;
            lvwSnack.FullRowSelect = true;
            lvwSnack.GridLines = true;

            lvwSnack.Columns.Add("No", 50, HorizontalAlignment.Left);
            lvwSnack.Columns.Add("ID Snack", 130, HorizontalAlignment.Center);
            lvwSnack.Columns.Add("Snack Name", 200, HorizontalAlignment.Center);
            lvwSnack.Columns.Add("Snack Amount", 150, HorizontalAlignment.Center);
        }

        private void loadDataMovie()
        {
            lvwMovie.Items.Clear();
            listMovie = _movieController.GetMovies();
            foreach (var trs in listMovie)
            {
                var noUrut = lvwMovie.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(trs.movieId.ToString());
                item.SubItems.Add(trs.movieName);
                item.SubItems.Add(trs.movieAmount.ToString());
                lvwMovie.Items.Add(item);
            }
        }

        private void loadDataSnack()
        {
            lvwSnack.Items.Clear();
            listSnack = _snackController.GetSnacks();
            foreach (var trs in listSnack)
            {
                var noUrut = lvwSnack.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(trs.snack_Id.ToString());
                item.SubItems.Add(trs.snackPackage);
                item.SubItems.Add(trs.amount.ToString());
                lvwSnack.Items.Add(item);
            }
        }

        private void HandleCreateMovie(Movie film)
        {
            loadDataMovie();

            /*            // tambahkan objek mhs yang baru ke dalam collection
                        listMovie.Add(film);
                        int noUrut = lvwMovie.Items.Count + 1;
                        // tampilkan data mhs yg baru ke list view
                        var item = new ListViewItem(noUrut.ToString());
                        item.SubItems.Add(film.movieId.ToString());
                        item.SubItems.Add(film.movieName);
                        item.SubItems.Add(film.movieAmount.ToString());
                        lvwMovie.Items.Add(item);*/
        }

        private void HandleCreateSnack(Snack snack)
        {
            loadDataSnack();
/*
            // tambahkan objek mhs yang baru ke dalam collection
            listSnack.Add(snack);
            int noUrut = lvwSnack.Items.Count + 1;
            // tampilkan data mhs yg baru ke list view
            var item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(snack.snack_Id.ToString());
            item.SubItems.Add(snack.snackPackage);
            item.SubItems.Add(snack.amount.ToString());
            lvwSnack.Items.Add(item);*/
        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();

            this.Visible = false;
        }

        private void btnInsert2_Click(object sender, EventArgs e)
        {
            InsertFilm addfilem = new InsertFilm("Tambah Data Transaksi", _movieController);
            addfilem.OnCreateData += HandleCreateMovie;
            addfilem.ShowDialog();
        }

        private void btnInsert1_Click(object sender, EventArgs e)
        {
            SnackInsert addSnack = new SnackInsert("Tambah Data Transaksi", _snackController);
            addSnack.OnCreateData += HandleCreateSnack;
            addSnack.ShowDialog();
        }

        private void btnRemove1_Click(object sender, EventArgs e)
        {
            if (lvwSnack.SelectedItems.Count > 0)
            {
                // ambil objek mhs yang mau dihapus dari collection
                Snack _snack = listSnack[lvwSnack.SelectedIndices[0]];

                int result = _snackController.senackDelete(_snack.snack_Id);

                if (result > 0) loadDataSnack();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRemove2_Click(object sender, EventArgs e)
        {
            if (lvwMovie.SelectedItems.Count > 0)
            {

                // ambil objek mhs yang mau dihapus dari collection
                Movie _movie = listMovie[lvwMovie.SelectedIndices[0]];

                int result = _movieController.movieDelete(_movie);



                if (result > 0) loadDataMovie();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
