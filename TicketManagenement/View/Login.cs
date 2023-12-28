using System;

using System.Windows.Forms;
using TicketManagenement.Controller;
using TicketManagenement.Model.Entity;


namespace TicketManagenement.View
{
    public partial class Login : Form
    {

        private userController _controller;
        private User usr;
        public static int userId;
        public static string name = "Guest";

        public Login()
        {
            InitializeComponent();
            _controller = new userController();

            txtPassword.UseSystemPasswordChar = true;
        }


        private void linkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sign_Up create = new Sign_Up();
            this.Close();
            create.ShowDialog();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            usr = new User();

            usr.Name = txtName.Text;
            usr.Password = txtPassword.Text;

            int result = _controller.Login(usr);

            if (result > 0)
            {
                if (int.TryParse(_controller.getUserId(txtName.Text), out  userId))
                {
                    name = txtName.Text;
                }
                name = txtName.Text;

                if (name == "Admin")
                {
                    name = "Admin";
                    manageSystem manage = new manageSystem();
                    manage.ShowDialog();

                    Visible = false;
                }
                else
                {
                    Home order = new Home();
                    order.ShowDialog();

                    Visible = false;
                }

            }
            else
            {
                txtName.Text = "";
                txtPassword.Text = "";
                txtName.Focus();
            }

        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
           Home order = new Home();
           order.ShowDialog();

           Visible = false;
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
