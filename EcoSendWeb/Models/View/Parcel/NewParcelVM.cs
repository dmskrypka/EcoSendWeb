using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EcoSendWeb.Models.View.Parcel
{
    public class NewParcelVM
    {
        [Required]
        [Display(Name = "Recipient first name")]
        public string RecipientFirstName { get; set; }

        [Required]
        [Display(Name = "Recipient last name")]
        public string RecipientLastName { get; set; }

        [EmailAddress]
        [Display(Name = "Recipient email")]
        public string RecipientEmail { get; set; }

        [Required]
        [Display(Name = "City")]
        public string DestCity { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string DestStreet { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string DestPostalCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string DestCountry { get; set; }

        [Required]
        [Display(Name = "Pack")]
        public int PackType { get; set; }

        public SelectList PackTypes { get; set; }

        [Display(Name = "Use yours points")]
        public int Points { get; set; }

    }
}