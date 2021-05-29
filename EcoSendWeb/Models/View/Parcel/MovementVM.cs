using System;
using System.ComponentModel.DataAnnotations;

namespace EcoSendWeb.Models.View.Parcel
{
    public class MovementVM
    {
        [DisplayFormat(DataFormatString = "{0:D4}")]
        [Display(Name = "Parcel Id")]
        public int ParcelId { get; set; }

        [Display(Name = "Pack type")]
        public string PackName { get; set; }

        [Display(Name = "Points")]
        public int Points { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        [Display(Name = "Date")]
        public DateTime CreatedDate { get; set; }
    }
}