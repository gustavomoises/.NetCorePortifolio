//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TableReady.Core.App.Models
{
    //Model View for Registration
    public class RegisterModelView
    {
        public int AuthId { get; set; }
        public int UserId { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        //public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
