//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class WaitlistEntry
    {
        public WaitlistEntry()
        {
            WaitlistEntryTable = new HashSet<WaitlistEntryTable>();
        }

        public int WaitlistEntryId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public short PartySize { get; set; }
        public string WaitlistStatus { get; set; }
        public string EntryOrigin { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime? SeatingDate { get; set; }
        public DateTime? CheckoutDate { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Restaurants Restaurant { get; set; }
        public virtual ICollection<WaitlistEntryTable> WaitlistEntryTable { get; set; }
    }
}
