//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class TableAvailabilityDates
    {
        public int TableId { get; set; }
        public DateTime Datetime { get; set; }
        public int TableGroupId { get; set; }
        public string AvailabilityStatus { get; set; }
        public string CheckOutStatus { get; set; }
        public string CleaningStatus { get; set; }

        public virtual TableInGroups Table { get; set; }
    }
}
