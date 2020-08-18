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
    //Model View for Owner
    public class OwnerModelView
    {
        public int RestaurantId { get; set; }
        public int OwnerId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Owner Name")]
        public string OwnerFullName { get; set; }
        public string Restaurant { get; set; }
        [Display(Name = "Owner Status")]
        public string Status { get; set; }
        [Display(Name="Apllication Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        [Display(Name = "New Request?")]
        public bool? RequestFlag { get; set; }
        [Display(Name = "Request Status")]
        public string RequestStatus { get; set; }
    }
}
