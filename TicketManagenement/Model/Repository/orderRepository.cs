using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using TicketManagenement.Model.Entity;
using TicketManagenement.Model.Context;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.IsisMtt;

namespace TicketManagenement.Model.Repository
{
    public  class orderRepository
    {
        private MySqlConnection _cnn;
        private Transaction _transaction;

        public orderRepository(DbContext context)
        {
            // membuka koneksi
            _cnn = context.ConnectionOpen();
        }

        // method untuk menambahkan transaction
        public int Input(Transaction transaksi, int userId)
        {
            int result = 0;
            string sql = "INSERT INTO transaction(user_id, movie, number, date, transaction_method, snack, transaction_amount) VALUES(@user_id, @movie, @number, @date, @transaction_method, @snack, @transaciton_amount)";
            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("user_id", userId);
                cmd.Parameters.AddWithValue("movie", transaksi.MovieName);
                cmd.Parameters.AddWithValue("number", transaksi.Number);
                cmd.Parameters.AddWithValue("date", transaksi.Date);
                cmd.Parameters.AddWithValue("transaction_method", transaksi.methode);
                cmd.Parameters.AddWithValue("snack", transaksi.Sncak);
                cmd.Parameters.AddWithValue("transaciton_amount", transaksi.Amount);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }

        // baca transaction berdasarkan user id
        public List<Transaction> readTransaction(int userId)
        {
            List<Transaction> list = new List<Transaction>();

            string sql = "SELECT *FROM transaction WHERE user_Id = @userId";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {   
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        _transaction = new Transaction();
                        _transaction.transactionID = int.Parse(dtr["transaction_id"].ToString());
                        _transaction.MovieName = dtr["movie"].ToString();
                        _transaction.Number = int.Parse(dtr["number"].ToString());
                        _transaction.Date = dtr["date"].ToString();
                        _transaction.methode = dtr["transaction_method"].ToString();
                        _transaction.Sncak = dtr["snack"].ToString();
                        _transaction.Amount = int.Parse(dtr["transaction_amount"].ToString());
                        list.Add(_transaction);
                    }
                }
            }
            return list;
        }

        // baca trnsaction berdasarkam  transactionid
        public List<Transaction> readTransactionId(int transactionId)
        {
            List<Transaction> list = new List<Transaction>();

            string sql = "SELECT *FROM transaction WHERE transaction_id = @transaction_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@transaction_id", transactionId);

                using (MySqlDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        _transaction = new Transaction();
                        _transaction.MovieName = dtr["movie"].ToString();
                        _transaction.Number = int.Parse(dtr["number"].ToString());
                        _transaction.Date = dtr["date"].ToString();
                        _transaction.methode = dtr["transaction_method"].ToString();
                        _transaction.Sncak = dtr["snack"].ToString();
                        _transaction.Amount = int.Parse(dtr["transaction_amount"].ToString());
                        list.Add(_transaction);
                    }
                }
            }
            return list;
        }


        // update transaction
        public int Update(Transaction transaction)
        {
            int result = 0;
            string sql = "UPDATE transaction SET number = @number, transaction_method = @transaction_method WHERE transaction_id = @transaction_id";
            
            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@transaction_id", transaction.transactionID);
                cmd.Parameters.AddWithValue("@number", transaction.Number);
                cmd.Parameters.AddWithValue("@transaction_method", transaction.methode);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            return result;
        }

        // method menghapus data transaction
        public int transactionDelete(int transactionID)
        {

            int result = 0;
            string sql = "DELETE FROM transaction WHERE Transaction_id = @transaction_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@transaction_id", transactionID);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);

                }
            }

            return result;
        }
    }
}
