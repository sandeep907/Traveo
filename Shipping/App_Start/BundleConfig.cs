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
                "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css", 
                "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.css"
               ));
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Content/bower_components/jquery/dist/jquery.min.js",
                "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/bower_components/fastclick/lib/fastclick.js",
                "~/Content/dist/js/adminlte.min.js",
                    "~/Scripts/Email.js",
                "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                "~/Content/plugins/iCheck/icheck.min.js",
                "~/Content/bower_components/moment/min/moment.min.js",
                    "~/Content/bower_components/jquery-knob/dist/jquery.knob.min.js",
                "~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                "~/Content/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                "~/Content/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js",
                "~/Content/bower_components/raphael/raphael.min.js",
                "~/Content/bower_components/morris.js/morris.min.js",
                 "~/Content/bower_components/chart.js/Chart.js",
                "~/Scripts/DashBoard/Common.js",
                "~/Scripts/Registration/Common.js",
                "~/Scripts/Roles/Common.js",
                "~/Scripts/Orders/Common.js"
                ));
            bundles.Add(new StyleBundle("~/LoginContent/css").Include(
               "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
               "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
               "~/Content/bower_components/Ionicons/css/ionicons.min.css",
               "~/Content/dist/css/AdminLTE.min.css",
               "~/Content/plugins/iCheck/square/blue.css"
              ));
            bundles.Add(new ScriptBundle("~/LoginContent/js").Include(
              "~/Content/bower_components/jquery/dist/jquery.min.js",
              "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
              "~/Content/plugins/iCheck/icheck.min.js"
              ));

        }
    }
}