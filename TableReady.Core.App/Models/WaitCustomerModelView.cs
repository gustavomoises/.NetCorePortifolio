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
    //Model View for Waitlist's entry
    public class WaitCustomerModelView
    {
        public int WaitlistEntryId { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        public int RestaurantID { get; set; }
        public string Restaurant { get; set; }
        [Display(Name = "Party Size")]
        public short PartySizew { get; set; }
        [Display(Name = "Status")]
        public string WaitlistStatus { get; set; }
        [Display(Name = "Origin")]
        public string EntryOriginw { get; set; }
        [Display(Name = "Position")]
        public string WaitlistPosition { get; set; }
    }
}
