//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class ReservationEntry
    {
        public ReservationEntry()
        {
            ReservationEntryTable = new HashSet<ReservationEntryTable>();
        }

        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public short PartySize { get; set; }
        public string ReservationStatus { get; set; }
        public string EntryOrigin { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ReservationDateTime { get; set; }
        public DateTime? CheckinDateTime { get; set; }
        public DateTime? SeatingDateTime { get; set; }
        public DateTime? CheckoutDateTime { get; set; }
        public string CustomerMessage { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Restaurants Restaurant { get; set; }
        public virtual ICollection<ReservationEntryTable> ReservationEntryTable { get; set; }
    }
}
