//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Tables
    {
        public Tables()
        {
            ReservationEntryTable = new HashSet<ReservationEntryTable>();
            TableInGroups = new HashSet<TableInGroups>();
            WaitlistEntryTable = new HashSet<WaitlistEntryTable>();
        }

        public int TableId { get; set; }
        public int? RestaurantId { get; set; }
        public string TableName { get; set; }
        public int TableMaxNumberSeats { get; set; }
        public string TableImage { get; set; }
        public string TableImageAvailable { get; set; }
        public string TableImageUnavailable { get; set; }
        public string TableImageCheckout { get; set; }
        public string TableImageCleaning { get; set; }
        public string TableType { get; set; }
        public string TableSize { get; set; }
        public DateTime CreationDate { get; set; }
        public bool TableActive { get; set; }

        public virtual Restaurants Restaurant { get; set; }
        public virtual ICollection<ReservationEntryTable> ReservationEntryTable { get; set; }
        public virtual ICollection<TableInGroups> TableInGroups { get; set; }
        public virtual ICollection<WaitlistEntryTable> WaitlistEntryTable { get; set; }
    }
}
