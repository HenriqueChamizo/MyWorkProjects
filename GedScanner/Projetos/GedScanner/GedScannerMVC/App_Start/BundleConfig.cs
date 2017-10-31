using System.Web;
using System.Web.Optimization;

namespace GedScannerMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/index").Include(
                    "~/Content/bootstrap/css/bootstrap.css",
                    "~/Content/dist/css/AdminLTE.css",
                    "~/Content/dist/css/skins/_all-skins.css",
                    "~/Content/bootstrap/css/style.css"));

            bundles.Add(new ScriptBundle("~/Content/js/index").Include(
                    "~/Content/bootstrap/js/bootstrap.min.js",
                    "~/Content/plugins/jQuery/jQuery-2.1.4.min.js",
                    "~/Content/dist/js/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/dynamsoft-dwt").Include(
                "~/Scripts/Resources/dynamsoft.webtwain.config.js",
                //"~/Scripts/Resources/dynamsoft.webtwain.initiate.js?t=150418",
                "~/Scripts/Resources/dynamsoft.webtwain.initiate.js",
                "~/Scripts/Resources/dynamsoft.webtwain.intellisense.js",
                //"~/Scripts/Resources/addon/dynamsoft.webtwain.addon.pdf.js", 
                "~/Scripts/online_demo_operation.js",
                "~/Scripts/online_demo_initpage.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/dynamsoft-dwt-2").Include(
                "~/Scripts/Resources/dynamsoft.webtwain.config.js",
                "~/Scripts/Resources/dynamsoft.webtwain.initiate.js",
                "~/Scripts/Resources/addon/dynamsoft.webtwain.addon.pdf.js",
                "~/Scripts/online_demo_operation.js",
                "~/Scripts/online_demo_initpage.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/elevate-zoom").Include(
                //"~/Scripts/jquery-1.8.3.min.js",
                "~/Scripts/jquery.elevateZoom-3.0.8.min.js",
                "~/Scripts/jquery.elevatezoom"
                ));

            //Admin LTE
            bundles.Add(new StyleBundle("~/AdminLTE/css").Include(
                "~/bootstrap/css/bootstrap.min.css",
                "~/dist/css/AdminLTE.css",
                "~/dist/css/skins/_all-skins.min.css"
                //"~/plugins/morris/morris.css",
                //"~/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                //"~/plugins/datepicker/datepicker3.css",
                //"~/plugins/daterangepicker/daterangepicker.css",
                //"~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
            ));

            bundles.Add(new ScriptBundle("~/AdminLTE/js").Include(
                //"~/plugins/morris/morris.min.js",
                //"~/plugins/sparkline/jquery.sparkline.min.js",
                //"~/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                //"~/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                //"~/plugins/knob/jquery.knob.js",
                //"~/plugins/daterangepicker/daterangepicker.js",
                //"~/plugins/datepicker/bootstrap-datepicker.js",
                //"~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                "~/plugins/slimScroll/jquery.slimscroll.min.js",
                //"~/plugins/fastclick/fastclick.js",
                "~/dist/js/app.min.js"
                //"~/dist/js/pages/dashboard.js",
                //"~/dist/js/demo.js"
                ));

            //Components
            bundles.Add(new StyleBundle("~/Styles/components").Include(
                "~/bootstrap/css/bootstrap.min.css",
                "~/Styles/INPUTHENRIQUE.css"));

            bundles.Add(new ScriptBundle("~/Scripts/components").Include(
                "~/plugins/jQuery/jquery-2.2.3.min.js",
                "~/bootstrap/js/bootstrap.min.js",
                "~/Scripts/INPUTHENRIQUE.JS"));

            //ICheck
            bundles.Add(new StyleBundle("~/plugins/iCheck/minimal/css").Include(
                      "~/plugins/iCheck/minimal/_all.css"));

            bundles.Add(new ScriptBundle("~/plugins/iCheck/minimal/js").Include(
                "~/plugins/iCheck/icheck.min.js"));

            //MaterialAdmin
            bundles.Add(new StyleBundle("~/plugins/materialadmin/css/theme-default").Include(
                "~/plugins/materialadmin/css/theme-default/bootstrap.css?1422792965",
                "~/plugins/materialadmin/css/theme-default/materialadmin.css?1425466319",
                "~/plugins/materialadmin/css/theme-default/font-awesome.min.css?1422529194",
                "~/plugins/materialadmin/css/theme-default/material-design-iconic-font.min.css?1421434286"));

            bundles.Add(new ScriptBundle("~/plugins/materialadmin/js/Form").Include(
                //"~/plugins/materialadmin/js/libs/jquery/jquery-1.11.2.min.js",
                //"~/plugins/materialadmin/js/libs/jquery/jquery-migrate-1.2.1.min.js",
                "~/plugins/materialadmin/js/libs/select2/select2.min.js",
                "~/plugins/materialadmin/js/libs/multi-select/jquery.multi-select.js", 
                "~/plugins/materialadmin/js/libs/bootstrap/bootstrap.min.js",
                "~/plugins/materialadmin/js/libs/spin.js/spin.min.js",
                "~/plugins/materialadmin/js/libs/autosize/jquery.autosize.min.js",
                "~/plugins/materialadmin/js/libs/nanoscroller/jquery.nanoscroller.min.js",
                "~/plugins/materialadmin/js/core/source/App.js",
                "~/plugins/materialadmin/js/core/source/AppNavigation.js",
                "~/plugins/materialadmin/js/core/source/AppOffcanvas.js",
                "~/plugins/materialadmin/js/core/source/AppCard.js",
                "~/plugins/materialadmin/js/core/source/AppForm.js",
                "~/plugins/materialadmin/js/core/source/AppNavSearch.js",
                "~/plugins/materialadmin/js/core/source/AppVendor.js",
                "~/plugins/materialadmin/js/core/demo/Demo.js"
                ));
        }
    }
}
