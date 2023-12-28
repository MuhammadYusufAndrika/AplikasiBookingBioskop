using System;
using System.Collections.Generic;

using System.Linq;
using System.Windows.Forms;
using TicketManagenement.Controller;
using TicketManagenement.Model.Entity;

namespace TicketManagenement.View
{
    public partial class InsertData : Form
    {
        // delegete
        public delegate void HandleCreateData(Transaction transaction);

        List<Movie> listMovie = new List<Movie>();
        List<Snack> listSnack = new List<Snack>();
        List<Transaction> transactionList = new List<Transaction>();

        private movieController _controller;
        private snackController _snackcontroller;
        private transactionController _transactionController;
        private Transaction transaction;

        int transacitonId;
        bool isNewData = true;
        int movieAmount = 0;
        int sanckAmount = 0;
        int accumulateAmount = 0;

        // event
        public event HandleCreateData OnCreateData;
        public event HandleCreateData OnUpdateData;

        public InsertData()
        {
            _transactionController = new transactionController();   
            _controller = new movieController();
            _snackcontroller = new snackController();
            InitializeComponent();

            ComboBoxMovie();
            ComboBoxSnack();
            accumulateAmount = movieAmount + sanckAmount;
            date.Value = DateTime.Now;
        }


        // memsaukan snack ke dalam combo box movie
        public void ComboBoxMovie()
        {
            movieDropDown.Items.Clear();
            listMovie = _controller.GetMovies();
            foreach (var movie in listMovie)
            {
                movieDropDown.Items.Add(movie.movieName.ToString());

                movieAmount = movie.movieAmount;
            }
        }

        // memsaukan snack ke dalam combo box snack
        public void ComboBoxSnack()
        {
            snackDropDown.Items.Clear();
            listSnack = _snackcontroller.GetSnacks();
            foreach (var snack in listSnack)
            {
                snackDropDown.Items.Add(snack.snackPackage.ToString());

                sanckAmount = snack.amount;
            }
        }

        public InsertData(string title, transactionController controller)
        : this()
        {
            this.Text = title;
            this._transactionController = controller;
        }
        public InsertData(string title, Transaction obj, transactionController controller)
        : this()
        {
            this.Text = title;
            this._transactionController = controller;
            isNewData = false;
            transaction = obj;

            lblInsertData.Text = "Update Data";

            transactionList = _transactionController.readTransactionId(transacitonId);
            foreach (var trs in transactionList)
            {
                movieDropDown.Items.Add(trs.MovieName);
                methodDropDown.Items.Add(trs.methode);
                txtNumber.Text = trs.Number.ToString();
                txtAmount.Text = trs.Amount.ToString();
                snackDropDown.Items.Add(trs.Sncak);
            }
        }


        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            Order back = new Order();
            back.ShowDialog();

            this.Close();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {

            if (isNewData) transaction = new Transaction();
            transaction.MovieName = movieDropDown.SelectedItem.ToString();
            transaction.Number = int.Parse(txtNumber.Text);
            transaction.Date = date.Value.ToString("yyyy-MM-dd");
            transaction.methode = methodDropDown.SelectedItem.ToString();
            transaction.Sncak = snackDropDown.SelectedItem.ToString();
            transaction.Amount = accumulateAmount;

            int result = 0;

            int id_pengguna = Login.userId;
            if (isNewData)
            {
                result = _transactionController.addTransction(transaction, id_pengguna);

                if (result > 0)
                {
                    OnCreateData(transaction);
                    this.Close();
                }
            }
            else
            {
                result = _transactionController.updateTransaction(transaction);
                transacitonId = transaction.transactionID;
                if (result > 0)
                {
                    OnUpdateData(transaction);
                    this.Close();
                }
            }
        }

        private void movieDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cek apakah item telah dipilih
            if (movieDropDown.SelectedIndex != -1)
            {
                // Ambil jumlah film terpilih dari listMovie
                int selectedMovieIndex = movieDropDown.SelectedIndex;
                movieAmount = listMovie[selectedMovieIndex].movieAmount;

                // Perbarui nilai accumulateAmount
                accumulateAmount = movieAmount;

                // Tampilkan nilai accumulateAmount pada txtAmount
                txtAmount.Text = accumulateAmount.ToString();
            }
        }

        private void snackDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cek apakah item telah dipilih
            if (snackDropDown.SelectedIndex != -1)
            {
                // Ambil jumlah makanan ringan terpilih dari listSnack
                int selectedSnackIndex = snackDropDown.SelectedIndex;
                sanckAmount = listSnack[selectedSnackIndex].amount;

                // Perbarui nilai accumulateAmount
                accumulateAmount = movieAmount + sanckAmount;

                // Tampilkan nilai accumulateAmount pada txtAmount
                txtAmount.Text = accumulateAmount.ToString();
            }
        }
    }
}
