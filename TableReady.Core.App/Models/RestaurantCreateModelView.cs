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
    //Model View for Restaurant
    public class RestaurantCreateModelView
    {
        public int RestaurantId { get; set; }
        public int OwnerId { get; set; }
        [Display(Name ="Restaurant Name")]
        public string RestaurantName { get; set; }
    }
}
