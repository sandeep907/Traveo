using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Shipping.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                "~/Content/bower_components/Ionicons/css/ionicons.min.css",
                  "~/Content/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                "~/Content/bower_components/bootstrap-daterangepicker/daterangepicker.css",
                  "~/Content/dist/css/AdminLTE.min.css",
                "~/Content/dist/css/skins/_all-skins.min.css",
                  "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
               ));

        }
    }
}