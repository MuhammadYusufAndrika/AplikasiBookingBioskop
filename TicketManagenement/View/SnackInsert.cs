using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketManagenement.Controller;
using TicketManagenement.Model.Entity;

namespace TicketManagenement.View
{
    public partial class SnackInsert : Form
    {
        public delegate void HandleCreateData(Snack snack);

        // panggil controller
        private snackController _snackController;
        private Snack _snack;

        bool isNewData = true;

        // event
        public event HandleCreateData OnCreateData;
        public event HandleCreateData OnUpdateData;

        public SnackInsert()
        {
            InitializeComponent();
        }

        public SnackInsert(string title, snackController controller)
        : this()
        {
            this.Text = title;
            this._snackController = controller;
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int result = 0;

            if (string.IsNullOrEmpty(txtName.Text) || !int.TryParse(txtPrice.Text, out int amount))
            {
                MessageBox.Show("Isi Nama Snack dan harga yang valid!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isNewData)
            {
                _snack = new Snack();
                {
                    _snack.snackPackage = txtName.Text;
                    _snack.amount = int.Parse(txtPrice.Text);
                };

                result = _snackController.addSnac(_snack);

                if (result > 0)
                {
                    OnCreateData(_snack);
                    this.Close();
                }
            }
        }
    }
}
