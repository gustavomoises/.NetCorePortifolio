//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class TableGroups
    {
        public TableGroups()
        {
            TableInGroups = new HashSet<TableInGroups>();
        }

        public int TableGroupId { get; set; }
        public int LayoutId { get; set; }
        public string TableGroupName { get; set; }
        public int? TableGroupPriority { get; set; }
        public decimal? TableGroupPosX { get; set; }
        public decimal? TableGroupPosY { get; set; }
        public string TableGroupImage { get; set; }
        public bool TableGroupActive { get; set; }

        public virtual Layouts Layout { get; set; }
        public virtual ICollection<TableInGroups> TableInGroups { get; set; }
    }
}
