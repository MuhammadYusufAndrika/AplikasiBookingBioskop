
using System;
using System.Windows.Forms;

namespace TicketManagenement.View
{
    public partial class Home : Form
    {
        public static string name;
        public Home()
        {
            InitializeComponent();
            name = lblUser.Text;
            lblUser.Text = Login.name;
            manageSystem();
        }

        // menghapus manage system jika bukan admin
        void manageSystem()
        {
            if(lblUser.Text != "Admin")
            {
                btnPaymentList.Dispose();
            }

        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Order addOrder = new Order();
            addOrder.ShowDialog();
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
           this.Dispose();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            if(lblUser.Text == "Guest")
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

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {

        }
    }
}
