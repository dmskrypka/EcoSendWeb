using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
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

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

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
