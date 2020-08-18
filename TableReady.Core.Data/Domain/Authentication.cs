//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;

namespace TableReady.Core.Data.Domain
{
    public partial class Authentication
    {
        public Authentication()
        {
            AuthenticationMatrix = new HashSet<AuthenticationMatrix>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<AuthenticationMatrix> AuthenticationMatrix { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
