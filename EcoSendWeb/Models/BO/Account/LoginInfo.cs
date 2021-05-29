using System;

namespace EcoSendWeb.Models.BO.Account
{
    public class LoginInfo
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public string UserAddress { get; set; }
    }
}