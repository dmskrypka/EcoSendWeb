using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using EcoSendWeb.App_Start;
using EcoSendWeb.Infrastructure;

namespace EcoSendWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static MvcApplication()
        {
        }

        protected void Application_Start()
        {
            MappingProfilesConfig.RegisterMapping();

            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ViewEngine());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FormAuth.Init();
        }

        private static readonly System.Text.RegularExpressions.Regex reMatchDomainName = new System.Text.RegularExpressions.Regex(@"^([\w\-]+\.)+((sk)|(cz)|(com)|(net)|(org)|(info))");

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            Uri uriSelf = app.Request.Url;

            UriBuilder ubNewAddress = new UriBuilder(uriSelf);

            //string strHostName = uriSelf.Host.ToLower();
            //if (rgOfficialNames.Length > 0 && !rgOfficialNames.Any(x => x == strHostName) && reMatchDomainName.IsMatch(strHostName))
            //{
            //    string strTld = strHostName.Split('.')[0].ToLowerInvariant();
            //    string strNewHost = rgOfficialNames.FirstOrDefault(q => q.Split('.')[0].ToLowerInvariant() == strTld);
            //    if (strNewHost == null)
            //    {
            //        strNewHost = rgOfficialNames[0];
            //    }

            //    if (!app.Request.IsLocal && ubNewAddress.Scheme == "http")
            //    {
            //        ubNewAddress.Scheme = "https";
            //        ubNewAddress.Port = -1;
            //    }

            //    ubNewAddress.Host = strNewHost;
            //    app.Response.StatusCode = 301;
            //    app.Response.AddHeader("Location", ubNewAddress.Uri.ToString());
            //    app.Response.End();
            //}
            //else
            //{
            //    if (!app.Request.IsLocal && ubNewAddress.Scheme == "http")
            //    {
            //        ubNewAddress.Scheme = "https";
            //        ubNewAddress.Port = -1;

            //        app.Response.Redirect(ubNewAddress.Uri.ToString(), true);
            //    }
            //}
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] is HttpCookie authCookie)
            {
                HttpContext.Current.User = FormAuth.GetPrincipal(authCookie);
            }
        }
    }
}
