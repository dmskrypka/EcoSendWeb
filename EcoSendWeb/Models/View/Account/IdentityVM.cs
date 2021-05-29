using EcoSendWeb.App_Start;
using EcoSendWeb.Models.BO.Account;
using EcoSendWeb.Services.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoSendWeb.Models.View.Account
{
    public class IdentityVM
    {
        public Guid UserId { get; private set; }

        public Guid Session { get; private set; }

        public string Email { get; private set; }

        public bool IsAdmin { get; private set; }

        public static IdentityVM GetActIdentity(IAccountServ accountServ, object session)
        {
            if(session != null && Guid.TryParse(session as String, out Guid guid))
            {
                RegisteredPerson rp = null; //accountServ.GetActUserBySession(guid);
                if (rp != null)
                {
                    return new IdentityVM
                    {
                        UserId = rp.Id,
                        Session = guid,
                        Email = rp.Email,
                        IsAdmin = rp.IsAdmin
                    };
                }
            }

            return null;
        }
    }
}