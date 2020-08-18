//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Owners
    {
        public Owners()
        {
            RestaurantOwners = new HashSet<RestaurantOwners>();
        }

        public int OwnerId { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<RestaurantOwners> RestaurantOwners { get; set; }
    }
}
