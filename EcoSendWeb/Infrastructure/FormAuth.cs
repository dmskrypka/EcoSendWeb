using EcoSendWeb.App_Start;
using EcoSendWeb.Models.BO.Account;
using EcoSendWeb.Services.Api;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Microsoft.Practices.Unity;

namespace EcoSendWeb.Infrastructure
{
    public enum UserRoles : uint
    {
        Unknown = 0,
        Client = 1,
        Worker = 2
    }

    public interface IExtendedPrincipal : IPrincipal
    {
        Guid Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        int Points { get; }
    }

    public class ExtendedPrincipal : IExtendedPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) 
        { 
            return FormAuth.IsUserInRole(this.Id, role); 
        }

        public ExtendedPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
    }

    public class ExtendedPrincipalSerializeModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
    }

    public class FormAuth
    {
        private static IAccountServ accountServ;
        public static void Init()
        {
            accountServ = UnityConfig.GetConfiguredContainer().Resolve<IAccountServ>();
        }

        public static void SetAuthCookie(UserBO user, HttpResponseBase response)
        {
            ExtendedPrincipalSerializeModel serializeModel = new ExtendedPrincipalSerializeModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Points = user.Points
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     version: 1,
                     name: user.Email,
                     issueDate: DateTime.Now,
                     expiration: DateTime.Now.AddMinutes(30),
                     isPersistent: false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            response.Cookies.Add(faCookie);
        }

        internal static bool IsUserInRole(Guid userId, string role)
        {
            return accountServ.IsUserInRole(userId, role);
        } 

        public static ExtendedPrincipal GetPrincipal(HttpCookie authCookie)
        {
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            ExtendedPrincipalSerializeModel serializeModel = serializer.Deserialize<ExtendedPrincipalSerializeModel>(authTicket.UserData);

            return new ExtendedPrincipal(authTicket.Name)
            {
                Id = serializeModel.Id,
                FirstName = serializeModel.FirstName,
                LastName = serializeModel.LastName,
                Email = serializeModel.Email,
                Points = serializeModel.Points
            };
        }
    }
}