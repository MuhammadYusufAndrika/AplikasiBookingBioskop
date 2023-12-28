using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagenement.Model.Entity
{
    public class Transaction
    {
        public int transactionID { get; set; }
        public string MovieName { get; set; }
        public int Number { get; set; }
        public string Date { get; set; }
        public string methode { get; set; }
        public string Sncak { get; set; }
        public int Amount { get; set; }

    }

}

