using System;

namespace TicketManagenement.Model.Entity
{
    public class User
    {
        public int User_id { get; set; }
        public string Name { get; set; }
        public string Alamat { get; set; }
        public int NoHp { get; set; }
        public string Password { get; set; }
    }
}
