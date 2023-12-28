using System;
using System.Windows.Forms;

using TicketManagenement.Model.Context;
using TicketManagenement.Model.Entity;
using TicketManagenement.Model.Repository;
using System.Collections.Generic;

namespace TicketManagenement.Controller
{
    public class userController
    {

        private userRepository _repository;


        // method memeriksa name sudah di pake atau belum
        public bool usernameValidasi(string username)
        {
            bool valid = false;
            using (DbContext context = new DbContext())
            {
                _repository = new userRepository(context);
                valid = _repository.DaftarValidasi(username);
            }
            return valid;
        }

        // method buat akum
        public int SignUp(User user)
        {
            bool valid = usernameValidasi(user.Name);

            int result = 0;

            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Alamat) ||
                string.IsNullOrEmpty(user.NoHp.ToString()) || string.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("Datamu Masi Belum Lengkap", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;
            }

            if (valid != true)
            {

                using (DbContext context = new DbContext())
                {
                    _repository = new userRepository(context);
                    result = _repository.signUp(user);
                }
            }
            else
            {
                MessageBox.Show("Username anda udah ada yang make", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil disimpan !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result;
        }

        // method login
        public int Login(User user)
        {
            int result = 0;
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("Isi datanya yang bener !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;
            }

            if (user.Name == "Admin" && user.Password == "Admin")
            {
                return 1;
            }
            else
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new userRepository(context);
                    result = _repository.Login(user);
                }

            }

            if (result > 0)
            {
                MessageBox.Show("Login Berhasil ! ! !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Login Gagal ! ! !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        // method mengambil userId
        public string getUserId(string name)
        {
            string userId = null;

            using (DbContext context = new DbContext())
            {
                _repository = new userRepository(context);
                userId = _repository.readUserId(name);
            }

            return userId;
        }

        // method menampilkan user yang sedang login
        public List<User> readUser(int userId)
        {
            List<User> list = new List<User>();

            using (DbContext context = new DbContext())
            {
                _repository = new userRepository(context);
                list = _repository.userData(userId);
            }

            return list;

        }

        // method untuk menghapus Account
        public int updateData(User usr, int userId)
        {
            int result = 0;
            if (usr.Name != usr.Name)
            {
                MessageBox.Show("Username tidak bisa diganti!!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            if(usr.Password != usr.Password)
            {
                MessageBox.Show("Password tidak bisa diganti!!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(usr.Alamat) || string.IsNullOrEmpty(usr.NoHp.ToString()))
            {
                MessageBox.Show("Lengkapi data!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new userRepository(context);
                result = _repository.updateAcc(usr, userId);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil diupdate !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal diupdate !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        // method untuk menhpaus akun
        public int deleteAccount(int userId)
        {
            int result = 0;

            var konfirmasi = MessageBox.Show("Apakah anda yakin ingin menghapus akun ini? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (konfirmasi == DialogResult.Yes)
            {
                using (DbContext context = new DbContext())
                {
                    _repository = new userRepository(context);
                    result = _repository.deleteAcc(userId);
                }

                MessageBox.Show("Account berhasil dihapus", "Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return result;
        }
    }
}

