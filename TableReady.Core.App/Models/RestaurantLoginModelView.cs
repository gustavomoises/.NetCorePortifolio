//Author: Gustavo Moises
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableReady.Core.App.Models
{
    //Model View for Restaurant's Login
    public class RestaurantLoginModelView
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
        public int RestaurantId { get; set; }
    }
}
