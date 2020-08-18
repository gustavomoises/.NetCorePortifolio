//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class ReservationEntryTable
    {
        public int TableId { get; set; }
        public int ReservationId { get; set; }

        public virtual ReservationEntry Reservation { get; set; }
        public virtual Tables Table { get; set; }
    }
}
