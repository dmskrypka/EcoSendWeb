using System;

namespace EcoSendWeb.Models.BO.Account
{
    public class UserBO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Points { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Pasport { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime Created { get; set; }
    }
}