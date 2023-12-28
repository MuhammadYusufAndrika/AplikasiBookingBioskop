using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using TicketManagenement.Model.Entity;
using TicketManagenement.Model.Context;

namespace TicketManagenement.Model.Repository
{
    public class sncakRepository
    {

        private MySqlConnection _cnn;
        private Snack _snanck;

        public sncakRepository(DbContext context)
        {
            // membnuka koneksi
            _cnn = (context.ConnectionOpen());
        }


        // membaca data snack
        public List<Snack> readSnack()
        {
            List<Snack> list = new List<Snack>();

            string sql = "SELECT *FROM snack";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                using (MySqlDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        _snanck = new Snack();
                        _snanck.snack_Id = int.Parse(dtr["snack_id"].ToString());
                        _snanck.snackPackage = dtr["snackName"].ToString();
                        _snanck.amount = int.Parse(dtr["snackAmount"].ToString());
                        list.Add(_snanck);
                    }
                }
            }

            return list;
        }

        // menambahkan snack
        public int insert(Snack snack)
        {
            int result = 0;

            string sql = "INSERT INTO snack(snackName, snackAmount) VALUES (@snackPackage, @snac_amount)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@snackPackage", snack.snackPackage);
                cmd.Parameters.AddWithValue("@snac_amount", snack.amount);

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

        // menghapus snack 
        public int snackDelete(int snackId)
        {

            int result = 0;
            string sql = "DELETE FROM snack WHERE snack_id = @snackId";



            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@snackId", snackId);

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
