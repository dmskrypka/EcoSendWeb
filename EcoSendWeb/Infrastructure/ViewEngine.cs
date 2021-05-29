using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace EcoSendWeb.Infrastructure
{
    public class ViewEngine : RazorViewEngine
    {
        private static string[] NewPartialViewFormats = new[] { 
            "~/Views/{1}/Partials/{0}.cshtml",
            "~/Views/Shared/Partials/{0}.cshtml",
            "~/Views/{0}.cshtml"
        };

        public ViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }
    }

    public static class IViewExtensions
    {
        public static string GetViewName(this IView view)
        {
            if (view is RazorView)
            {
                string viewUrl = ((RazorView)view).ViewPath;
                string viewFileName = viewUrl.Substring(viewUrl.LastIndexOf('/'));
                string viewFileNameWithoutExtension = Path.GetFileNameWithoutExtension(viewFileName);
                return (viewFileNameWithoutExtension);
            }
            else
            {
                throw (new InvalidOperationException("This view is not a WebFormView"));
            }
        }
    }
}