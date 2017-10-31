using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Handlers;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace Chamizo.Web.Control
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FindAddress runat=server></{0}:FindAddress>")]
    public class FindAddress : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        public string Padding
        {
            get
            {
                String s = (String)ViewState["Padding"];
                return ((s == null) ? "" : s);
            }

            set
            {
                ViewState["Padding"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            //try
            //{
            //    #region String
            //    //StringBuilder sb = new StringBuilder();

            //    //string js = @"var inputsCEP = $('#logradouro, #bairro, #localidade, #uf, #ibge');
            //    //            var inputsRUA = $('#cep, #bairro, #ibge');
            //    //                            var validacep = /^[0 - 9]{ 8}$/;
            //    //                            function limpa_formulário_cep(alerta) {
            //    //                                if (alerta !== undefined)
            //    //                                {
            //    //                                    alert(alerta);
            //    //                                }
            //    //                                inputsCEP.val('');
            //    //                            }
            //    //                            function get(url) {
            //    //              $.get(url, function(data) {
            //    //                                    if (!('erro' in data)) {
            //    //                                        if (Object.prototype.toString.call(data) === '[object Array]')
            //    //                                        {
            //    //                                            var data = data[0];
            //    //                                        }
            //    //                  $.each(data, function(nome, info) {
            //    //                    $('#' + nome).val(nome === 'cep' ? info.replace(/\D/g, '') : info).attr('info', nome === 'cep' ? info.replace(/\D/g, '') : info);
            //    //                  });
            //    //                                    } else {
            //    //                                        limpa_formulário_cep('CEP não encontrado.');
            //    //                                    }
            //    //                                });
            //    //                            }
            //    //            // Digitando RUA/CIDADE/UF
            //    //            $('#logradouro, #localidade, #uf').on('blur', function(e) {
            //    //                                if ($('#logradouro').val() !== '' && $('#logradouro').val() !== $('#logradouro').attr('info') && $('#localidade').val() !== '' && $('#localidade').val() !== $('#localidade').attr('info') && $('#uf').val() !== '' && $('#uf').val() !== $('#uf').attr('info')) {
            //    //                                    inputsRUA.val('...');
            //    //                                    get('https://viacep.com.br/ws/' + $('#uf').val() + '/' + $('#localidade').val() + '/' + $('#logradouro').val() + '/json/');
            //    //                                }
            //    //                            });
            //    //            // Digitando CEP
            //    //            $('#cep').on('blur', function(e) {
            //    //                                var cep = $('#cep').val().replace(/\D/g, '');
            //    //                                if (cep !== '' && validacep.test(cep))
            //    //                                {
            //    //                                    inputsCEP.val('...');
            //    //                                    get('https://viacep.com.br/ws/' + cep + '/json/');
            //    //                                }
            //    //                                else
            //    //                                {
            //    //                                    limpa_formulário_cep(cep == '' ? undefined : 'Formato de CEP inválido.');
            //    //                                }
            //    //                            }); ";
            //    //string html = @"<script src='https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js'></script>
            //    //                <form method='get' action='.'>
            //    //                     <label> Cep:
            //    //                    <input name='cep' type='text' id='cep' value='' size='10' maxlength='9'>
            //    //                             </label>
            //    //                             <br />
            //    //                             <label> Rua:
            //    //                    <input name='rua' type='text' id='logradouro' size='60' />
            //    //                         </label>
            //    //                         <br />
            //    //                         <label> Bairro:
            //    //                    <input name='bairro' type='text' id='bairro' size='40' />
            //    //                         </label>
            //    //                         <br />
            //    //                         <label> Cidade:
            //    //                    <input name='cidade' type='text' id='localidade' size='40' />
            //    //                         </label>
            //    //                         <br />
            //    //                         <label> Estado:
            //    //                    <input name='uf' type='text' id='uf' size='2' />
            //    //                         </label>
            //    //                         <br />
            //    //                         <label> IBGE:
            //    //                    <input name='ibge' type='text' id='ibge' size='8' />
            //    //                         </label>
            //    //                         <br />
            //    //                       </form> ";
            //    //sb.Append("<script>" + js + "</script>");
            //    //sb.AppendFormat("<div id=\"{0}\" name=\"{0}\" style=\"width: {1}; heigth: {2}\">", 
            //    //    base.ClientID, base.Width.ToString(), base.Height.ToString());
            //    //sb.Append(html);
            //    //sb.Append("</div>");
            //    //output.Write(sb.ToString());

            //    ////output.Write(Text);
            //    #endregion

            //    TextBox txtCep = new TextBox();
            //    txtCep.Visible = true;
            //}
            //catch (Exception ex)
            //{
            //    output.Write(ex.Message);
            //}
        }

        public Address GetAddressBy(string PostalCode)
        {
            WebClient viaCep = new WebClient();
            viaCep.BaseAddress = "https://viacep.com.br/ws/" + PostalCode + "/json/";
            Stream s = viaCep.OpenRead(viaCep.BaseAddress);
            StreamReader sr = new StreamReader(s);
            string json = sr.ReadToEnd(); 
            Address ad = GetAddressByJson(json);
            return ad;
        }

        public Address GetAddressByJson(string Json)
        {
            Address address = new Address();
            Json = Json.Replace("\r", "").Replace("\n", "");
            Json = Json.Replace("{", "").Replace("}", "");

            string[] split = Json.Split(new String[] { "," }, StringSplitOptions.None);
            for(int i = 0; i < split.Count(); i++)
            {
                string[] fields = split[i].Replace("\"", "").Split(new String[] { ":" }, StringSplitOptions.None);
                string campo = fields[0].Trim();
                switch (campo)
                {
                    case "cep":
                        address.PostalCode = fields[1];
                        break;
                    case "logradouro":
                        address.Street = fields[1];
                        break;
                    case "complemento":
                        address.Complement = fields[1];
                        break;
                    case "bairro":
                        address.District = fields[1];
                        break;
                    case "localidade":
                        address.City = fields[1];
                        break;
                    case "uf":
                        address.State = fields[1];
                        break;
                    case "ibge":
                        address.StateCode = fields[1];
                        break;
                    case "gia":
                        address.GiaCode = fields[1];
                        break;
                }
            }
            return address;
        }
    }
}
