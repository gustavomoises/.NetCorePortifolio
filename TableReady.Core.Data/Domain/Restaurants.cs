//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Restaurants
    {
        public Restaurants()
        {
            AuthenticationMatrix = new HashSet<AuthenticationMatrix>();
            Layouts = new HashSet<Layouts>();
            ReservationEntry = new HashSet<ReservationEntry>();
            RestaurantEmployees = new HashSet<RestaurantEmployees>();
            RestaurantOwners = new HashSet<RestaurantOwners>();
            Tables = new HashSet<Tables>();
            WaitlistEntry = new HashSet<WaitlistEntry>();
        }

        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int? LayoutActive { get; set; }

        public virtual Layouts LayoutActiveNavigation { get; set; }
        public virtual ICollection<AuthenticationMatrix> AuthenticationMatrix { get; set; }
        public virtual ICollection<Layouts> Layouts { get; set; }
        public virtual ICollection<ReservationEntry> ReservationEntry { get; set; }
        public virtual ICollection<RestaurantEmployees> RestaurantEmployees { get; set; }
        public virtual ICollection<RestaurantOwners> RestaurantOwners { get; set; }
        public virtual ICollection<Tables> Tables { get; set; }
        public virtual ICollection<WaitlistEntry> WaitlistEntry { get; set; }
    }
}
