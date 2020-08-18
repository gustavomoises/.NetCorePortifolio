//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Employees
    {
        public Employees()
        {
            RestaurantEmployees = new HashSet<RestaurantEmployees>();
        }

        public int EmployeeId { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<RestaurantEmployees> RestaurantEmployees { get; set; }
    }
}
