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
    //Model View for Customer's Reservation
    public class ResCustomerModelView
    {
        [Display(Name = "Check-in")]
        public bool Checkin { get; set; }
        public int ReservationEntryId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int RestaurantID { get; set; }
        public string Restaurant { get; set; }
        [Display(Name = "Party Size")]
        public short PartySize { get; set; }
        [Display(Name = "Status")]
        public string ReservationStatus { get; set; }
        [Display(Name = "Origin")]
        public string EntryOrigin { get; set; }
        [Display(Name = "Date and Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "New Date and Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        public DateTime ReservationDate_new { get; set; }
        public string Message { get; set; }

    }
}
