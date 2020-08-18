//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Customers
    {
        public Customers()
        {
            ReservationEntry = new HashSet<ReservationEntry>();
            WaitlistEntry = new HashSet<WaitlistEntry>();
        }

        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<ReservationEntry> ReservationEntry { get; set; }
        public virtual ICollection<WaitlistEntry> WaitlistEntry { get; set; }
    }
}
