using System;

using System.Windows.Forms;
using System.Collections.Generic;
using TicketManagenement.Controller;
using TicketManagenement.Model.Entity;

namespace TicketManagenement.View
{
    public partial class Profile : Form
    {

        private userController _controller  = new userController();
        private User user = new User();
        List<User> userlist = new List<User>();
        int userId = Login.userId;

        public Profile()
        {
            InitializeComponent();

            if(Login.name == "Admin")
            {
                lblName.Dispose();
                lblAddress.Dispose();
                lblPassword.Dispose();
                lblPhoneNumber.Dispose();
                txtName.Dispose();
                txtAddres.Dispose();
                txtNoHp.Dispose();
                txtPass.Dispose();
            }

            userlist = _controller.readUser(userId);
            txtPass.UseSystemPasswordChar = true;
            txtName.ReadOnly = true;
            txtAddres.ReadOnly = true;
            txtNoHp.ReadOnly = true;
            txtPass.ReadOnly = true;


            foreach (var user in userlist)
            {
                txtName.Text = user.Name;
                txtAddres.Text = user.Alamat;
                txtNoHp.Text = user.NoHp.ToString();
                txtPass.Text = user.Password;
            }

        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Home order = new Home();
            order.ShowDialog();

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var konfirmasi = MessageBox.Show("Apakah anda yakin untuk keluar", "Informasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(konfirmasi == DialogResult.Yes)
            {
                Home.name = "Guest";
                Login.name = "Guest";
                Home home = new Home();
                home.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int hapus = _controller.deleteAccount(userId);

            if (hapus > 0)
            {
                Home.name = "Guest";
                Login.name = "Guest";
                Home login = new Home();
                this.Close();
                login.ShowDialog();
            }
        }
    }
}
