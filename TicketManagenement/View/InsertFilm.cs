using System;
using System.Collections.Generic;
using System.Windows.Forms;

using TicketManagenement.Model.Entity;
using TicketManagenement.Controller;
using System.Transactions;
using static Bunifu.UI.WinForms.Helpers.Transitions.Transition;

namespace TicketManagenement.View
{
    public partial class InsertFilm : Form
    {
        // delegete
        public delegate void HandleCreateData(Movie movie);

        // panggil controller
        private movieController _movieController;
        private Movie _movie;

        bool isNewData = true;

        // event
        public event HandleCreateData OnCreateData;
        public event HandleCreateData OnUpdateData;

        public InsertFilm()
        {
            InitializeComponent();
        }

        public InsertFilm(string title, movieController controller)
        : this()
        {
            this.Text = title;
            this._movieController = controller;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int result = 0;

            if (string.IsNullOrEmpty(txtMovie.Text) || !int.TryParse(txtPrice.Text, out int amount))
            {
                MessageBox.Show("Isi Nama Filem dan harga yang valid!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isNewData)
            {
                _movie = new Movie();
                {
                    _movie.movieName = txtMovie.Text;
                    _movie.movieAmount = int.Parse(txtPrice.Text);
                };

                result = _movieController.addMovie(_movie);

                if (result > 0)
                {
                    OnCreateData(_movie);
                    this.Close();
                }
            }
        }
    }
}
