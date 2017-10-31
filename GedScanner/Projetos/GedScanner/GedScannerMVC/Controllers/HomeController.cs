using GedScannerMVC.ClassBD;
using GedScannerMVC.ClassFrm;
using GedScannerMVC.ClassView;
using Model;
using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GedScannerMVC.Controllers
{
    public class HomeController : GEDController
    {
        HomeBD bd;
        public static string loginName = "Usuario";

        // GET: Home
        public ActionResult Index()
        {
            LoginsInternos login = Session[loginName] as LoginsInternos;
            if (login == null)
            {
                CookieLogin cookie = GetCookie();
                if (cookie != null)
                    return View("Bloqueado", cookie);
                else
                {
                    Home home = new Home();
                    home.SessionOff();
                    return View(home);
                }
            }
            else
                return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult GetExtern(string email)
        {
            Home home = new Home
            {
                btnValue = "Selecionar"
            };
            LoginsInternos loginInterno;
            if (!string.IsNullOrEmpty(email))
            {
                TypesErrors erro = new TypesErrors();
                bd = new HomeBD();
                loginInterno = new LoginsInternos
                {
                    LOGI_EMAIL = email
                };

                if (bd.GetExternLoginInterno(ref loginInterno, ref erro))
                    Session.Add(loginName, loginInterno);

                return RedirectToAction("Troca");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PostExtern(string email)
        {
            Home home = new Home
            {
                btnValue = "Selecionar"
            };
            LoginsInternos loginInterno;
            if (!string.IsNullOrEmpty(email))
            {
                TypesErrors erro = new TypesErrors();
                bd = new HomeBD();
                loginInterno = new LoginsInternos
                {
                    LOGI_EMAIL = email
                };

                if (bd.GetExternLoginInterno(ref loginInterno, ref erro))
                    Session.Add(loginName, loginInterno);

                return RedirectToAction("Troca");
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult Login(FrmHomeIndex form)
        {
            Home home = new Home
            {
                btnValue = "Selecionar"
            };
            LoginsInternos loginInterno;
            if (string.IsNullOrEmpty(form.cliente) && !Convert.ToBoolean(form.errobanco))
            {
                if (string.IsNullOrEmpty(form.email))
                    home.ErroEmail = true;

                if (string.IsNullOrEmpty(form.senha))
                    home.ErroSenha = true;

                if (!home.ErroEmail && !home.ErroSenha)
                {
                    TypesErrors erro = new TypesErrors();
                    bd = new HomeBD();
                    loginInterno = new LoginsInternos
                    {
                        LOGI_EMAIL = form.email,
                        LOGI_PWD = form.senha
                    };

                    if (bd.GetLoginInterno(ref loginInterno, ref erro))
                    {
                        if (loginInterno.LOGI_ATIVO)
                        {
                            Session.Add(loginName, loginInterno);
                            home.panelSelCliente = true;

                            List<Cliente> Clientes = loginInterno.clientes;

                            home.clientes = Clientes;
                        }
                        else
                        {
                            home.panelInativo = true;
                        }
                    }
                    else
                        home.ErroLogin = true;
                }
                else
                    home.ErroLogin = true;

                if (home.ErroLogin || 
                    home.ErroEmail || 
                    home.ErroSenha)
                    home.btnValue = "Entrar";
            }
            else
            {
                loginInterno = Session[loginName] as LoginsInternos;

                int index = Convert.ToInt32(form.cliente);

                Cliente cliente = loginInterno.clientes.Find(cli => cli.CLI_IND == index);
                if (cliente != null)
                {
                    TypesErrors erro = new TypesErrors();
                    bd = new HomeBD();
                    ClienteDatabase database = new ClienteDatabase();
                    if (bd.GetClienteDataBase(cliente, ref database, ref erro))
                    {
                        if (database.BASE_IND != 0)
                        {
                            cliente.DATABASE = database;
                            loginInterno.cliente = cliente;
                            Session[loginName] = loginInterno;
                            SetCookie();
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            home.panelSelCliente = true;
                            home.ErroBanco = true;
                            home.clientes = loginInterno.clientes;
                        }
                    }
                    else
                    {
                        home.panelSelCliente = true;
                        home.ErroBanco = true;
                        home.clientes = loginInterno.clientes;
                    }
                }
            }

            return View("Index", home);
        }

        public ActionResult Sair()
        {
            Session.Remove(loginName);
            Response.Cookies.Remove(loginName);

            Home home = new Home
            {
                panelLogin = true,
                btnValue = "Entrar"
            };

            return View("Index", home);
        }

        public ActionResult Troca()
        {
            LoginsInternos login = Session[loginName] as LoginsInternos;
            login.cliente = null;
            Session[loginName] = login;

            Home home = new Home
            {
                panelSelCliente = true,
                btnValue = "Selecionar",
                clientes = login.clientes
            };
            SetCookie();
            return View("Index", home);
        }

        public ActionResult Bloqueado()
        {
            CookieLogin loginCookie = GetCookie();
            Session.Remove(loginName);
            return View(loginCookie);
        }

        public CookieLogin GetCookie()
        {
            HttpCookie loginCookie = Request.Cookies.Get(loginName);
            CookieLogin cookie = null;
            if (loginCookie != null)
            {
                cookie = new CookieLogin
                {
                    LOGI_IND = loginCookie.Values["LOGI_IND"].ToString(),
                    LOGI_NOME = loginCookie.Values["LOGI_NOME"].ToString(),
                    LOGI_EMAIL = loginCookie.Values["LOGI_EMAIL"].ToString(),
                    CLI_IND = loginCookie.Values["CLI_IND"].ToString(),
                    CLI_RAZAOSOCIAL = loginCookie.Values["CLI_RAZAOSOCIAL"].ToString()
                };
            }
            return cookie;
        }

        public bool SetCookie()
        {
            try
            {
                LoginsInternos loginSession = Session[loginName] as LoginsInternos;

                Request.Cookies.Remove(loginName);
                HttpCookie loginCookie = new HttpCookie(loginName);
                loginCookie.Values.Add("LOGI_IND", loginSession.LOGI_IND.ToString());
                loginCookie.Values.Add("LOGI_NOME", loginSession.LOGI_NOME);
                loginCookie.Values.Add("LOGI_EMAIL", loginSession.LOGI_EMAIL);
                loginCookie.Values.Add("CLI_IND", loginSession.cliente.CLI_IND.ToString());
                loginCookie.Values.Add("CLI_RAZAOSOCIAL", loginSession.cliente.CLI_RAZAOSOCIAL);
                loginCookie.Expires = DateTime.Now.AddDays(1);
                loginCookie.HttpOnly = true;
                Response.Cookies.Add(loginCookie);
                return true;
            }
            catch { return false; }
        }
    }
}