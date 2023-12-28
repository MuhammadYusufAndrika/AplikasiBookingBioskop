using System;
using System.Collections.Generic;

using System.Windows.Forms;
using TicketManagenement.Model.Context;
using TicketManagenement.Model.Entity;
using TicketManagenement.Model.Repository;

namespace TicketManagenement.Controller
{
    public class transactionController
    {
        private orderRepository _repository;

        //menampilkan daftar transaction
        public List<Transaction> read(int userId)
        {
            List<Transaction> list = new List<Transaction>();

            using (DbContext context = new DbContext())
            {
                _repository = new orderRepository(context);
                list = _repository.readTransaction(userId);
            }

            return list;
        }

        // read transacitonid
        public List<Transaction> readTransactionId(int transactionId)
        {
            List<Transaction> list = new List<Transaction>();

            using (DbContext context = new DbContext())
            {
                _repository = new orderRepository(context);
                list = _repository.readTransactionId(transactionId);
            }

            return list;
        }


        // menambahkan data film
        public int addTransction(Transaction transaction, int userId)
        {
            int result = 0;

            if (string.IsNullOrEmpty(transaction.Number.ToString()))
            {
                MessageBox.Show("Nomor kursi blm ada !!!", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new orderRepository(context);
                result = _repository.Input(transaction, userId);
            }

            return result;
        }

        // mengupdate date transaction
        public int updateTransaction(Transaction transaction)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new orderRepository(context);
                result = _repository.Update(transaction);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil diupdate !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal diupdate !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        // menghapus data film
        public int transactionDelete(int transactionId)
        {
            int result = 0;

            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menghapus data ini? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (konfirmasi == DialogResult.Yes)
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new orderRepository(context);
                    result = _repository.transactionDelete(transactionId);
                }

                MessageBox.Show("Data berhasil dihapus", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return result;
        }

    }
}