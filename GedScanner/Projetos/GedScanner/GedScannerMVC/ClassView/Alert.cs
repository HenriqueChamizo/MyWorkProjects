using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Enuns;

namespace GedScannerMVC.ClassView
{
    public class Alert
    {
        public TypesBootstrapColors type_color { get; set; }
        public string title { get; set; }
        public string text { get; set; }

        public static Alert getAlertDanger(string title = null, string text = "")
        {
            Alert alert = new Alert();
            alert.type_color = TypesBootstrapColors.danger;
            alert.title = title;
            alert.text = text;
            return alert;
        }

        public static Alert getAlertInfo(string title = null, string text = "")
        {
            Alert alert = new Alert();
            alert.type_color = TypesBootstrapColors.info;
            alert.title = title;
            alert.text = text;
            return alert;
        }

        public static Alert getAlertPrimary(string title = null, string text = "")
        {
            Alert alert = new Alert();
            alert.type_color = TypesBootstrapColors.primary;
            alert.title = title;
            alert.text = text;
            return alert;
        }

        public static Alert getAlertSuccess(string title = null, string text = "")
        {
            Alert alert = new Alert();
            alert.type_color = TypesBootstrapColors.success;
            alert.title = title;
            alert.text = text;
            return alert;
        }

        public static Alert getAlertWarning(string title = null, string text = "")
        {
            Alert alert = new Alert();
            alert.type_color = TypesBootstrapColors.warning;
            alert.title = title;
            alert.text = text;
            return alert;
        }
    }
}