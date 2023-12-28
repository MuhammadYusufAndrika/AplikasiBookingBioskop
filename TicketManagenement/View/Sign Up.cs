using System;

using System.Windows.Forms;
using TicketManagenement.Controller;
using TicketManagenement.Model.Entity;

namespace TicketManagenement.View
{
    public partial class Sign_Up : Form
    {
        // memanggil controller dan entity
        private User user;
        private userController _controller;

        public Sign_Up()
        {
            InitializeComponent();
            _controller = new userController();

            txtConPass.UseSystemPasswordChar = true;
            txtPassword.UseSystemPasswordChar = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           Login login = new Login();
           login.ShowDialog(); 

            Visible = false;
        }

        private void Sign_Up_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.ShowDialog();

            Visible = false;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtConPass.Text)
            {
                User user = new User();

                user.Name = txtName.Text;
                user.Alamat = txtAddress.Text;
                
                // validasi jika no hp kosong maka tidak akan di konversi jadi int
                if (int.TryParse(txtPhoneNumber.Text, out int number))
                {
                    user.NoHp = number;
                }

                user.Password = txtPassword.Text;

                int result = _controller.SignUp(user);
                bool valid = _controller.usernameValidasi(user.Name);

                if (result > 0)
                {
                    Login login = new Login();
                    login.ShowDialog();

                    Visible = false;
                }

                if (valid == true)
                {
                    txtName.Text = "";
                    txtName.Focus();
                }
            }
            else
            {
                MessageBox.Show("Password Tidak Sama!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConPass.Text = "";
                txtConPass.Focus();
            }
        }

    }
}
