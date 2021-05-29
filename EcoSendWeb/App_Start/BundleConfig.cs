using System.Web.Optimization;

namespace EcoSendWeb.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                    "~/Content/bootstrap/bootstrap.css",
                    "~/Content/bootstrap-datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/slicknav/css").Include(
                    "~/Content/slicknav.css"));

            bundles.Add(new StyleBundle("~/Content/themes/eco/css").Include(
                    "~/Content/themes/eco/eco.ui.forms.css",
                    "~/Content/themes/eco/eco.ui.dialogs.css",
                    "~/Content/themes/eco/eco.ui.site.css"));
        }
    }
}
