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
    //Model View for Record
    public class RecordModelView
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public int CustomerId { get; set; }
        public string Restaurant { get; set; }
        [Display(Name = "Date and Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy hh:mm}")]
        public DateTime RecordDate { get; set; }
        [Display(Name = "Party Size")]
        public short PartySize { get; set; }
        public string Status { get; set; }
        [Display(Name = "Origin")]
        public string EntryOrigin { get; set; }
        public string Message { get; set; }




    }
}
