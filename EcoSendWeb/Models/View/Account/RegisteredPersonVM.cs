using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoSendWeb.Models.View.Account
{
    public class RegisteredPersonVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Display(Name = "IsValid")]
        public bool IsValid { get; set; }

        [Display(Name = "IsAdmin")]
        public bool IsAdmin { get; set; }

        public SelectList IsValidList
        {
            get
            {
                return new SelectList(new List<bool>
                {
                    true,
                    false
                });
            }
        }

        [Display(Name = "Registration date")]
        public string StrRegistrationDate
        {
            get => this.RegistrationDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}