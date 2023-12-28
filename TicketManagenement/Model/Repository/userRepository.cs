using System;

using System.Windows.Forms;
using TicketManagenement.Model.Context;
using TicketManagenement.Model.Entity;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace TicketManagenement.Model.Repository
{
    public class userRepository
    {
        private MySqlConnection _cnn;
        private User user;

        public userRepository(DbContext context)
        {
            // membnuka koneksi
            _cnn = (context.ConnectionOpen());
        }

        // Validasi akun sudah terbuat atau belum
        public bool DaftarValidasi(string name)
        {
            bool valid = false;
            try
            {
                string sql = "SELECT name FROM users WHERE name = @name";
                using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Get User and Pass Error: {0}", ex.Message);
            }
            return valid;
        }

        // method signUp
        public int signUp(User usrSIgnUp)
        {
            int result = 0;
            string sql = "INSERT INTO Users(name, alamat, No_Hp, password) VALUES (@name, @alamat, @No_Hp, @password)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@name", usrSIgnUp.Name);
                cmd.Parameters.AddWithValue("@alamat", usrSIgnUp.Alamat);
                cmd.Parameters.AddWithValue("@No_Hp", usrSIgnUp.NoHp );
                cmd.Parameters.AddWithValue("@password", usrSIgnUp.Password);

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

        // method untuk melakukan login
        public int Login(User usrLogin)
        {
            int result = 0;

            string sql = "SELECT *FROM users WHERE name = @name AND password = @password";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@name", usrLogin.Name);
                cmd.Parameters.AddWithValue("@password", usrLogin.Password);

                try
                {
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }

        // membaca user id
        public string readUserId(string name)
        {
            string userId = null;

            string sql = "SELECT user_id FROM users WHERE name = @name";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@name", name);

                MySqlDataReader dtr = cmd.ExecuteReader();
                if (dtr.Read())
                {
                    userId = dtr["user_id"].ToString();
                }
            }

            return userId;
        }

        // menampilkan user yang sedanng login
        public List<User> userData(int user_id)
        {
            List<User> list = new List<User>(); // Menggunakan tipe data User

            try
            {
                string sql = "SELECT * FROM users WHERE user_id = @user_id";

                using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            user = new User();
                            user.User_id = user_id;
                            user.Name = dtr["name"].ToString();
                            user.Alamat = dtr["alamat"].ToString();
                            user.NoHp = int.Parse(dtr["no_hp"].ToString());
                            user.Password = dtr["password"].ToString();
                            list.Add(user);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Error: {0}", ex.Message);

            }

            return list;
        }

        // update profile
        public int updateAcc(User usr, int userId)
        {
            int result = 0;

            string sql = "UPDATE SET alamat = @alamat, no_hp = @no_hp WHERE  user_id = @user_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@alamat", usr.Alamat);
                cmd.Parameters.AddWithValue("@no_hp", usr.NoHp);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Error: {0}", ex.Message);
                }
            }

            return result;
        }

        // hapus account
        public int deleteAcc(int userId)
        {
            int result = 0;

            string sql = "DELETE FROM users WHERE user_id = @user_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _cnn))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Error: {0}", ex.Message);

                }
            }

            return result;
        }
    }

}

