using System;

namespace EcoSendWeb.Models.BO.Account
{
    public class RegisteredPerson
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsValid { get; set; }
        public bool IsAdmin { get; set; }

    }
}