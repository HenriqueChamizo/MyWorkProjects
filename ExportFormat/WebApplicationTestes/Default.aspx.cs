using Chamizo.Web.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationTestes
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cep_TextChanged(object sender, EventArgs e)
        {
            TextBox ceptxt = sender as TextBox;
            string cep = ceptxt.Text.Replace("-", "").Replace(".", "");
            if (cep.Count() == 8)
            {
                Address end = new Address();
                FindAddress find = new FindAddress();
                end = find.GetAddressBy(cep);
                lbrua.Text = lbrua.Text + ": " + end.Street;
                lbbairro.Text = lbbairro.Text + ": " + end.District;
                lbgia.Text = lbgia.Text + ": " + end.GiaCode;
                lbibge.Text = lbibge.Text + ": " + end.StateCode;
            }
            else
                return;
        }

        protected void cep_DataBinding(object sender, EventArgs e)
        {
            TextBox ceptxt = sender as TextBox;
            string cep = ceptxt.Text.Replace("-", "").Replace(".", "");
            if (cep.Count() == 8)
            {
                Address end = new Address();
                FindAddress find = new FindAddress();
                end = find.GetAddressBy(cep);
                lbrua.Text = lbrua.Text + ": " + end.Street;
                lbbairro.Text = lbbairro.Text + ": " + end.District;
                lbgia.Text = lbgia.Text + ": " + end.GiaCode;
                lbibge.Text = lbibge.Text + ": " + end.StateCode;
            }
            else
                return;
        }
    }
}