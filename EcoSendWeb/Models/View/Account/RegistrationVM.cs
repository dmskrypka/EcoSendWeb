using System.ComponentModel.DataAnnotations;

namespace EcoSendWeb.Models.View.Account
{
    public class RegistrationVM
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

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}