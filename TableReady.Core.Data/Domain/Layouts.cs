//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOSusing System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Layouts
    {
        public Layouts()
        {
            Restaurants = new HashSet<Restaurants>();
            TableGroups = new HashSet<TableGroups>();
        }

        public int LayoutId { get; set; }
        public int RestaurantId { get; set; }
        public string LayoutName { get; set; }
        public string LayoutImage { get; set; }

        public virtual Restaurants Restaurant { get; set; }
        public virtual ICollection<Restaurants> Restaurants { get; set; }
        public virtual ICollection<TableGroups> TableGroups { get; set; }
    }
}
