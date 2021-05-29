using System.ComponentModel.DataAnnotations;

namespace EcoSendWeb.Models.View.Account
{
    public class UserVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "PostalCode")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Pasport")]
        public string Pasport { get; set; }
    }
}