using System;

using TicketManagenement.Model.Entity;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketManagenement.Controller;

namespace TicketManagenement.View
{
    public partial class Order : Form
    {

        List<Transaction> listorders = new List<Transaction>();
        public transactionController _controller = new transactionController();
        int userId = Login.userId;

        public Order()
        {
            InitializeComponent();
            lblUser.Text = Login.name;

            manageSystem();
            InisialisasiOrder();
            ReloadDataTransaksi();
        }

        // menghapus manage system jika bukan admin
        private void manageSystem()
        {
            if (lblUser.Text != "Admin")
            {
                btnPaymentList.Dispose();
            }
        }

        private void ReloadDataTransaksi()
        {
            if (userId != 0)
            {
                LoadDataTransaksi(); // Memuat data transaksi jika userId tidak sama dengan 0
            }
        }
        private void InisialisasiOrder()
        {
            lvwOrder.View = System.Windows.Forms.View.Details;
            lvwOrder.FullRowSelect = true;
            lvwOrder.GridLines = true;

            lvwOrder.Columns.Add("No", 50, HorizontalAlignment.Left);
            lvwOrder.Columns.Add("ID Transaction", 90, HorizontalAlignment.Center);
            lvwOrder.Columns.Add("Movie Name", 150, HorizontalAlignment.Center);
            lvwOrder.Columns.Add("Number", 70, HorizontalAlignment.Center);
            lvwOrder.Columns.Add("Date", 150, HorizontalAlignment.Center);
            lvwOrder.Columns.Add("Trasaction Method", 150, HorizontalAlignment.Center);
            lvwOrder.Columns.Add("Snack", 150, HorizontalAlignment.Center);
            lvwOrder.Columns.Add("Amount", 150, HorizontalAlignment.Center);
        }

        private void LoadDataTransaksi()
        {
            lvwOrder.Items.Clear();
            listorders = _controller.read(userId);

            foreach (var trs in listorders)
            {
                var noUrut = lvwOrder.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(trs.transactionID.ToString());
                item.SubItems.Add(trs.MovieName);
                item.SubItems.Add(trs.Number.ToString());
                item.SubItems.Add(trs.Date);
                item.SubItems.Add(trs.methode);
                item.SubItems.Add(trs.Sncak);
                item.SubItems.Add(trs.Amount.ToString());
                lvwOrder.Items.Add(item);
            }
        }

        private void OnCreateEventHandler(Transaction trs)
        {
            LoadDataTransaksi();

            /*// tambahkan objek mhs yang baru ke dalam collection
              listorders.Add(trs);
              int noUrut = lvwOrder.Items.Count + 1;
              // tampilkan data mhs yg baru ke list view
              ListViewItem item = new ListViewItem(noUrut.ToString());
              item.SubItems.Add(trs.transactionID.ToString());
              item.SubItems.Add(trs.MovieName);
              item.SubItems.Add(trs.Number.ToString());
              item.SubItems.Add(trs.Date);
              item.SubItems.Add(trs.methode);
              item.SubItems.Add(trs.Sncak);
              item.SubItems.Add(trs.Amount.ToString());
              lvwOrder.Items.Add(item);*/
        }

        private void OnUpdateEventHandler(Transaction trs)
        {

            LoadDataTransaksi();
            /*// ambil index data mhs yang edit
              int index = lvwOrder.SelectedIndices[0];
              // update informasi mhs di listview
              ListViewItem itemRow = lvwOrder.Items[index];
              itemRow.SubItems[1].Text = trs.transactionID.ToString();
              itemRow.SubItems[2].Text = trs.MovieName;
              itemRow.SubItems[3].Text = trs.Number.ToString();
              itemRow.SubItems[4].Text = trs.Date;
              itemRow.SubItems[5].Text = trs.methode;
              itemRow.SubItems[6].Text = trs.Sncak;
              itemRow.SubItems[7].Text = trs.Amount.ToString();*/
        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home back = new Home();
            back.ShowDialog();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            if (lblUser.Text == "Guest")
            {
                Login login = new Login();
                login.ShowDialog();
            }
            else
            {
                Profile profile = new Profile();
                profile.ShowDialog();
            }
        }

        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            if (lblUser.Text == "Guest")
            {
                MessageBox.Show("Tolong Login Dulu", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                InsertData InsertData = new InsertData("Update Data Transaksi", _controller);
                InsertData.OnCreateData += OnCreateEventHandler;
                InsertData.ShowDialog();
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (lvwOrder.SelectedItems.Count > 0)
            {
                Transaction trs = listorders[lvwOrder.SelectedIndices[0]];
                InsertData updateData = new InsertData("Update Data Transaksi", trs, _controller);
                updateData.OnUpdateData += OnUpdateEventHandler;
                updateData.ShowDialog();
            }
            else
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwOrder.SelectedItems.Count > 0)
            {

                // ambil objek mhs yang mau dihapus dari collection
                Transaction _order = listorders[lvwOrder.SelectedIndices[0]];

                int result = _controller.transactionDelete(_order.transactionID);


                if (result > 0) LoadDataTransaksi();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
