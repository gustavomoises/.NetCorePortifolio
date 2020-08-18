//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class AuthenticationMatrix
    {
        public int AuthenticationId { get; set; }
        public int RestaurantId { get; set; }
        public string Role { get; set; }

        public virtual Authentication Authentication { get; set; }
        public virtual Restaurants Restaurant { get; set; }
    }
}
