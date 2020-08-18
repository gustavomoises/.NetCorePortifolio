//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Users
    {
        public Users()
        {
            Customers = new HashSet<Customers>();
            Employees = new HashSet<Employees>();
            Owners = new HashSet<Owners>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int AuthenticationId { get; set; }

        public virtual Authentication Authentication { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<Owners> Owners { get; set; }
    }
}
