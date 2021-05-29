using EcoSendWeb.Models.BO.Parcel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EcoSendWeb.Models.View.Parcel
{
    public class ParcelVM
    {
        [DisplayFormat(DataFormatString = "{0:D4}")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.00}")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "City")]
        public string DestCity { get; set; }

        [Display(Name = "Street")]
        public string DestStreet { get; set; }

        [Display(Name = "Postal Code")]
        public string DestPostalCode { get; set; }

        [Display(Name = "Country")]
        public string DestCountry { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        [Display(Name = "Created")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Approved")]
        public DateTime? ConfirmedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        [Display(Name = "Received")]
        public DateTime? ReceivedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        [Display(Name = "Paid")]
        public DateTime? PaidDate { get; set; }

        [Display(Name = "Recipient")]
        public string RecipientFullName { get; set; }

        [Display(Name = "Sender")]
        public string SenderFullName { get; set; }

        [Display(Name = "Pack")]
        public int PackType { get; set; }

        [Display(Name = "Pack points")]
        public int PackPoints { get; set; }

        public SelectList PackTypes { get; set; }

        public Guid RecipientId { get; set; }

        [Display(Name = "Is recycled")]
        public bool IsRecycled { get; set; }
    }
}