using System;
using System.Collections.Generic;

using System.Windows.Forms;
using TicketManagenement.Model.Entity;
using TicketManagenement.Model.Context;
using TicketManagenement.Model.Repository;

namespace TicketManagenement.Controller
{
    public class snackController
    {
        private sncakRepository _repository;

        // menampilkan data snack
        public List<Snack> GetSnacks()
        {
            List<Snack> list = new List<Snack>();

            using (DbContext context = new DbContext())
            {
                _repository = new sncakRepository(context);
                list = _repository.readSnack();
            }

            return list;
        }

        // menambahkan snack
        public int addSnac(Snack snack)
        {
            int result = 0;
            if (string.IsNullOrEmpty(snack.snackPackage))
            {
                MessageBox.Show("Nama Snack blm ada !!!", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            if (string.IsNullOrEmpty(snack.snackPackage))
            {
                MessageBox.Show("Harga Snack blm ada !!!", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new sncakRepository(context);
                result = _repository.insert(snack);
            }

            return result;
        }

        // menghapus data sncak
        public int senackDelete(int sncakId)
        {
            int result = 0;

            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menghapus data ini? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (konfirmasi == DialogResult.Yes)
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new sncakRepository(context);
                    result = _repository.snackDelete(sncakId);
                }

                if(result > 0){
                    MessageBox.Show("Data berhasil dihapus", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            return result;
        }
    }
}
