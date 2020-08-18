//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class TableInGroups
    {
        public TableInGroups()
        {
            TableAvailabilityDates = new HashSet<TableAvailabilityDates>();
        }

        public int TableId { get; set; }
        public int TableGroupId { get; set; }
        public int MaxTableSeatNumber { get; set; }
        public decimal? TablePosX { get; set; }
        public decimal? TablePosY { get; set; }
        public string RestaurantArea { get; set; }
        public string RestaurantZone { get; set; }
        public int? RestaurantFloor { get; set; }

        public virtual Tables Table { get; set; }
        public virtual TableGroups TableGroup { get; set; }
        public virtual ICollection<TableAvailabilityDates> TableAvailabilityDates { get; set; }
    }
}
