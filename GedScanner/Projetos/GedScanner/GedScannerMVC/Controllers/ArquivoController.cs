using GedScannerMVC.ClassBD;
using Model;
using Model.Config;
using Model.Enuns;
using Persits.PDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GedScannerMVC.Controllers
{
    public class ArquivoController : GEDController
    {
        private ArquivoBD BD;

        // GET: Arquivo
        public ActionResult Index()
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            ClassView.Arquivo view = new ClassView.Arquivo();
            if (id != null)
            {
                try
                {
                    int ind = id.Value;
                    BD = new ArquivoBD(Session);
                    TypesErrors erro = new TypesErrors();
                    List<PlanoContas> planoscontas = new List<PlanoContas>();
                    List<Conta> contas = new List<Conta>();
                    List<Model.Ged.ArquivoTipo> arquivosTipos = new List<Model.Ged.ArquivoTipo>();
                    List<Model.Ged.Campo> campos = new List<Model.Ged.Campo>();
                    List<Model.Ged.CampoValor> valores = new List<Model.Ged.CampoValor>();
                    Model.Ged.Arquivo arquivo = new Model.Ged.Arquivo();
                    arquivo.GEDARQ_IND = id.Value;
                    BD.GetArquivoById(ref arquivo, ref planoscontas, ref contas, ref arquivosTipos, ref campos, ref valores, ref erro);

                    byte[] byImg = arquivo.GEDARQ_ARQUIVO;
                    arquivo.GEDARQ_ARQUIVO = null;
                    string resourceImg = OrganizaDiretorioEArquivo(byImg);

                    string resource = Request.Url.OriginalString.ToString().Replace("/Arquivo", "").Replace("/Detail", "").Replace("/" + ind.ToString(), "");
                    view.GEDARQ_DESCRICAO = arquivo.GEDARQ_DESCRICAO;
                    view.resourceImg = (resource + resourceImg).Replace("//", "/").Replace(":/", "://");

                    view.arquivo = arquivo;
                    view.planoscontas = planoscontas;
                    view.tipos = arquivosTipos;
                    view.contas = contas;
                    view.hiddenIndices = "";
                    return View(view);
                }
                catch (Exception ex)
                {
                    view.exception = ex.Message;
                    return View(view);
                }
            }
            else
                return View("Index");
        }

        [HttpPost]
        public PartialViewResult FormDinamico(int idArquivo, int idTipo, int idAtual)
        {
            Thread.Sleep(500);
            ClassView.Listas pview = new ClassView.Listas();
            BD = new ArquivoBD(Session);
            TypesErrors erro = new TypesErrors();
            if (idAtual != -1)
            {
                List<Model.Ged.CampoValorDetail> valoresD = BD.GetCamposValoresByArquivo(idArquivo, idTipo, ref erro);
                List<Model.Ged.Campo> campos = new List<Model.Ged.Campo>();
                List<Model.Ged.CampoValor> valores = new List<Model.Ged.CampoValor>();
                foreach (Model.Ged.CampoValorDetail valor in valoresD)
                {
                    campos.Add(valor.CAPVAL_CAMPO);
                    Model.Ged.CampoValor cvalor = new Model.Ged.CampoValor();
                    cvalor.CAPVAL_IND = valor.CAPVAL_IND;
                    cvalor.CAPVAL_VALOR = valor.CAPVAL_VALOR;
                    cvalor.CAPVAL_DATA = valor.CAPVAL_DATA;
                    cvalor.CAPVAL_CONTARQUIVOSTIPO = valor.CAPVAL_CONTARQUIVOSTIPO == null ? 0 : valor.CAPVAL_CONTARQUIVOSTIPO.CATIP_IND;
                    cvalor.CAPVAL_CAMPO = valor.CAPVAL_CAMPO.CAMP_IND;
                    cvalor.CAPVAL_ATUAL = valor.CAPVAL_ATUAL;
                    valores.Add(cvalor);
                }

                pview.campos = campos;
                pview.valores = valores;
            }
            else
            {
                List<Model.Ged.CampoDetail> camposD = BD.GetCamposByArquivoTipo(idTipo, ref erro);
                List<Model.Ged.Campo> campos = new List<Model.Ged.Campo>();
                Model.Ged.Campo campo;
                foreach (Model.Ged.CampoDetail campod in camposD)
                {
                    campo = new Model.Ged.Campo();
                    campo.CAMP_IND = campod.CAMP_IND;
                    campo.CAMP_DESCRICAO = campod.CAMP_DESCRICAO;
                    campo.CAMP_ARQUIVOTIPO = campod.CAMP_ARQUIVOTIPO;
                    campo.CAMP_DT_INICIO = campod.CAMP_DT_INICIO;
                    campo.CAMP_LOGININSERT = campod.CAMP_LOGININSERT;
                    campo.CAMP_OBRIGATORIO = campod.CAMP_OBRIGATORIO;
                    campo.CAMP_TIPO = campod.CAMP_TIPO;
                    campos.Add(campo);
                }

                pview.campos = campos;
            }
            return PartialView("FormDinamico", pview);

        }
        
        [HttpPost]
        public ActionResult Novo(string hidden)
        {
            if (!CheckSessions())
                return RedirectToAction("Index", "Home");

            return View("Novo", Convert.ToInt32(hidden));
        }

        [HttpGet]
        public PartialViewResult Teste(int? teste)
        {
            if (teste != null)
                return PartialView("Teste", (teste.Value.ToString() + " Teste       ....."));
            else
                return PartialView("Teste", "Vazio isso cara!");
        }

        [HttpPost]
        public PartialViewResult Teste(string teste)
        {
            return PartialView("Teste", (teste + " Teste       ....."));
        }

        private string OrganizaDiretorioEArquivo(byte[] image)
        {
            string retorno;
            try
            {
                string folderResource = "Resource/";
                string pathServerBasic = Server.MapPath(".") + "/../../";
                string fileName = Convert.ToString(DateTime.Now.ToFileTime()) + ".png";

                LoginsInternos login = Session["Usuario"] as LoginsInternos;
                string empresa = login.cliente.CLI_CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "") + "/";
                string loginInd = login.LOGI_IND.ToString().Replace(" ", "") + "/";

                #region Verifica Pastas
                if (Directory.Exists(pathServerBasic + folderResource))
                {//Se existe a pasta 'Resource'
                    pathServerBasic = pathServerBasic + folderResource;
                    if (Directory.Exists(pathServerBasic + empresa))
                    {//Se existe a pasta 'CNPJ da Empresa selecionada'
                        pathServerBasic = pathServerBasic + empresa;
                        if (Directory.Exists(pathServerBasic + loginInd))
                        {//Se existe a pasta 'indice do usuario logado'
                         //Limpa pasta excluindo e incluindo de novo
                            pathServerBasic = pathServerBasic + loginInd;
                            DeleteDiretorios(pathServerBasic);
                            Directory.CreateDirectory(pathServerBasic);
                        }
                        else
                        {//Se não existe a pasta 'indice do usuario logado'
                         //Cria pasta 'indice do usuario logado'
                            pathServerBasic = pathServerBasic + loginInd;
                            Directory.CreateDirectory(pathServerBasic);
                        }
                    }
                    else
                    {//Se não existe a pasta 'CNPJ da Empresa selecionada'
                        pathServerBasic = pathServerBasic + empresa;

                        //Cria pasta 'CNPJ da Empresa selecionada'
                        Directory.CreateDirectory(pathServerBasic);
                        if (Directory.Exists(pathServerBasic + loginInd))
                        {//Se existe a pasta 'indice do usuario logado'
                         //Limpa pasta excluindo e incluindo de novo
                            pathServerBasic = pathServerBasic + loginInd;
                            DeleteDiretorios(pathServerBasic);
                            Directory.CreateDirectory(pathServerBasic);
                        }
                        else
                        {//Se não existe a pasta 'indice do usuario logado'
                         //Cria pasta 'indice do usuario logado'
                            pathServerBasic = pathServerBasic + loginInd;
                            Directory.CreateDirectory(pathServerBasic);
                        }
                    }
                }
                else
                {//Se não existe a pasta 'Resource'
                    pathServerBasic = pathServerBasic + folderResource;

                    //Cria pasta 'Resource'
                    Directory.CreateDirectory(pathServerBasic);
                    if (Directory.Exists(pathServerBasic + empresa))
                    {//Se existe a pasta 'CNPJ da Empresa selecionada'
                        pathServerBasic = pathServerBasic + empresa;
                        if (Directory.Exists(pathServerBasic + loginInd))
                        {//Se existe a pasta 'indice do usuario logado'
                         //Limpa pasta excluindo e incluindo de novo
                            pathServerBasic = pathServerBasic + loginInd;
                            DeleteDiretorios(pathServerBasic);
                            Directory.CreateDirectory(pathServerBasic);
                        }
                        else
                        {//Se não existe a pasta 'indice do usuario logado'
                         //Cria pasta 'indice do usuario logado'
                            pathServerBasic = pathServerBasic + loginInd;
                            Directory.CreateDirectory(pathServerBasic);
                        }
                    }
                    else
                    {//Se não existe a pasta 'CNPJ da Empresa selecionada'
                        pathServerBasic = pathServerBasic + empresa;

                        //Cria pasta 'CNPJ da Empresa selecionada'
                        Directory.CreateDirectory(pathServerBasic);
                        if (Directory.Exists(pathServerBasic + loginInd))
                        {//Se existe a pasta 'indice do usuario logado'
                         //Limpa pasta excluindo e incluindo de novo
                            pathServerBasic = pathServerBasic + loginInd;
                            DeleteDiretorios(pathServerBasic);
                            Directory.CreateDirectory(pathServerBasic);
                        }
                        else
                        {//Se não existe a pasta 'indice do usuario logado'
                         //Cria pasta 'indice do usuario logado'
                            pathServerBasic = pathServerBasic + loginInd;
                            Directory.CreateDirectory(pathServerBasic);
                        }
                    }
                }
                #endregion

                FileStream fs = new FileStream((pathServerBasic + "/" + fileName).Replace("//", "/"), FileMode.CreateNew, FileAccess.Write);
                fs.Write(image, 0, image.Length);
                fs.Flush();
                fs.Close();
                retorno = "/" + folderResource + "/" + empresa + "/" + loginInd + "/" + fileName;
                retorno = retorno.Replace("//", "/");
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        private void DeleteDiretorios(string path)
        {
            string[] diretories = Directory.GetDirectories(path);
            string[] subdiretories;
            string[] files = Directory.GetFiles(path);
            string[] subfiles;

            for (int x = 0; x < files.Length; x++)
            {
                System.IO.File.Delete(files[x]);
            }

            for (int x = 0; x < diretories.Length; x++)
            {
                subfiles = Directory.GetFiles(diretories[x]);
                for (int fx = 0; fx < subfiles.Length; fx++)
                {
                    System.IO.File.Delete(subfiles[fx]);
                }
                subdiretories = Directory.GetDirectories(diretories[x]);
                for (int y = 0; y < subdiretories.Length; y++)
                {
                    DeleteDiretorios(subdiretories[y]);
                }
                Directory.Delete(diretories[x]);
            }
        }

        public void Salvar(int? id)
        {
            string strImageName = "";
            try
            {
                int iFileLength;
                HttpFileCollectionBase files = HttpContext.Request.Files;
                HttpPostedFileBase uploadfile = files["RemoteFile"];
                if (uploadfile != null)
                {
                    LoginsInternos login = Session["Usuario"] as LoginsInternos;
                    string Empresa = "Empresa_" + login.cliente.CLI_IND.ToString();
                    string Usuario = "Usuario_" + login.LOGI_IND.ToString();
                    strImageName = uploadfile.FileName;
                    string strFilePath = Server.MapPath(".") + "\\UploadedImages\\Collect\\" + Empresa + "\\" + Usuario;
                    if (!Directory.Exists(strFilePath))
                        Directory.CreateDirectory(strFilePath);
                    else
                    {
                        DeleteDiretorios(strFilePath);
                        Directory.CreateDirectory(strFilePath);
                    }
                    DateTime now = DateTime.Now;
                    string strData = now.ToString("yyyyMMdd_HHmmss_") + now.Millisecond + "_" + (new Random().Next() % 1000).ToString();
                    strFilePath = strFilePath + "\\" + strData + strImageName;
                    uploadfile.SaveAs(strFilePath);

                    iFileLength = uploadfile.ContentLength;

                    byte[] inputBuffer = new byte[iFileLength];
                    Stream inputStream;

                    inputStream = uploadfile.InputStream;
                    inputStream.Read(inputBuffer, 0, iFileLength);

                    string strConnString;
                    DAO.Connection connecion = new DAO.Connection(login.cliente.DATABASE.BASE_SERVER,
                                                                    login.cliente.DATABASE.BASE_NOMEBANCO,
                                                                    login.cliente.DATABASE.BASE_USUARIO,
                                                                    login.cliente.DATABASE.BASE_SENHA);

                    strConnString = connecion.connectionstring;

                    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnString);

                    string SqlCmdText = "";

                    
                    string strPath = strFilePath.Replace("\\" + strData + strImageName, "");

                    Pdf pdf = new Pdf();
                    PdfManager objPDF = pdf.pdfmanager;
                    PdfDocument objDoc = objPDF.OpenDocument(strFilePath);

                    for (int i = 1; i <= objDoc.Pages.Count; i++)
                    {
                        string si = i.ToString();
                        SqlCmdText = SqlCmdText +
                        @"insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_LOTE) 
                                    values (@GEDARQ_DISPONIVELEM" + si +
                                          ",@GEDARQ_DISPONIVELPOR" + si +
                                          ",@GEDARQ_ARQUIVO" + si +
                                          ",@GEDARQ_TAMANHO" + si +
                                          ",@GEDARQ_EXTENSAO" + si +
                                          ",@GEDARQ_DESCRICAO" + si +
                                          ",@GEDARQ_LOTE" + si + ") ";
                    }

                    System.Data.SqlClient.SqlCommand sqlCmdObj = new System.Data.SqlClient.SqlCommand(SqlCmdText, sqlConnection);

                    for (int i = 1; i <= objDoc.Pages.Count; i++)
                    {
                        string si = i.ToString();
                        string imgName = strPath + "\\" + strData + "_" + i.ToString() + ".jpg";
                        using (FileStream imageStream = new FileStream(imgName, FileMode.Create))
                        {

                            // Create preview for Page 1 at 50% scale.
                            PdfPage objPage = objDoc.Pages[i];
                            PdfPreview objPreview = objPage.ToImage();


                            inputBuffer = objPreview.SaveToMemory();
                            iFileLength = inputBuffer.Length;
                            //Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(300);
                            //Aspose.Pdf.Devices.PngDevice pngDevice = new Aspose.Pdf.Devices.PngDevice(resolution);
                            //pngDevice.Process(pdfDocument.Pages[i], imageStream);

                            //inputBuffer = new byte[imageStream.Length];
                            //imageStream.Close();
                            //FileStream imgStream = new FileStream(imgName, FileMode.Open);
                            //imgStream.Read(inputBuffer, 0, Convert.ToInt32(imgStream.Length));

                            //string[] split = strImageName.Split(new string[] { "." }, StringSplitOptions.None);
                            //string extend = split[split.Length - 1];
                            string extend = "jpg";

                            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELEM" + si, now);
                            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELPOR" + si, login.LOGI_IND);
                            sqlCmdObj.Parameters.Add("@GEDARQ_ARQUIVO" + si, System.Data.SqlDbType.Binary, inputBuffer.Length).Value = inputBuffer;
                            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_TAMANHO" + si, iFileLength);
                            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_EXTENSAO" + si, extend);
                            sqlCmdObj.Parameters.Add("@GEDARQ_DESCRICAO" + si, System.Data.SqlDbType.VarChar, 255).Value = 
                                strImageName.Replace("." + extend, "").Replace(".bmp", "").Replace(".pdf", "")+ "_" + si;
                            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_LOTE" + si, Convert.ToInt32(id));
                            //imgStream.Close();
                        }
                    }
                    inputStream.Close();
                    objDoc.Close();
                    sqlConnection.Open();
                    int r = sqlCmdObj.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //string pdf = "20170302115244002.pdf";
            //string img = "20170302115244002.jpg";
            //string path = @"D:\Projetos\SemTeamFundation\GedScanner\Projetos\TestePDF\";

            //string MyPdfPath = path + pdf;
            //Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(MyPdfPath);
            //using (FileStream imageStream = new FileStream(path + img, FileMode.Create))
            //{
            //    Aspose.Pdf.Devices.Resolution resolution = new Aspose.Pdf.Devices.Resolution(300);
            //    Aspose.Pdf.Devices.PngDevice pngDevice = new Aspose.Pdf.Devices.PngDevice(resolution);
            //    pngDevice.Process(pdfDocument.Pages[1], imageStream);
            //    imageStream.Close();
            //}

            #region comments
            //ImageBuffer image = Session["Images"] as ImageBuffer;
            //if (image == null)
            //{
            //    string strImageName = "";
            //    try
            //    {
            //        int iFileLength;
            //        HttpFileCollectionBase files = HttpContext.Request.Files;
            //        HttpPostedFileBase uploadfile = files["RemoteFile"];
            //        if (uploadfile != null)
            //        {
            //            strImageName = uploadfile.FileName;
            //            string strFilePath = Server.MapPath(".") + "\\UploadedImages\\Collect";
            //            if (!System.IO.Directory.Exists(strFilePath))
            //            {
            //                System.IO.Directory.CreateDirectory(strFilePath);
            //            }
            //            DateTime now = DateTime.Now;
            //            string strData = now.ToString("yyyyMMdd_HHmmss_") + now.Millisecond + "_" + (new Random().Next() % 1000).ToString();
            //            strFilePath = strFilePath + "\\" + strData + strImageName;
            //            uploadfile.SaveAs(strFilePath);

            //            iFileLength = uploadfile.ContentLength;

            //            Byte[] inputBuffer = new Byte[iFileLength];
            //            System.IO.Stream inputStream;

            //            inputStream = uploadfile.InputStream;
            //            inputStream.Read(inputBuffer, 0, iFileLength);

            //            image = new ImageBuffer();
            //            image.id = id.Value;
            //            image.inputBuffer = inputBuffer;
            //            //image.length = length.Value;

            //            image.strFilePath = strFilePath;
            //            image.listBuffers = new List<byte[]>();
            //            image.listBuffers.Add(inputBuffer);

            //            #region Comments
            //            //String strConnString;
            //            //Model.LoginsInternos login = Session["Usuario"] as Model.LoginsInternos;
            //            //DAO.Connection connecion = new DAO.Connection(login.cliente.DATABASE.BASE_SERVER,
            //            //                                              login.cliente.DATABASE.BASE_NOMEBANCO,
            //            //                                              login.cliente.DATABASE.BASE_USUARIO,
            //            //                                              login.cliente.DATABASE.BASE_SENHA);

            //            //strConnString = connecion.connectionstring;

            //            //System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnString);

            //            //String SqlCmdText =
            //            //@"insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_LOTE) 
            //            //             values (@GEDARQ_DISPONIVELEM,@GEDARQ_DISPONIVELPOR,@GEDARQ_ARQUIVO,@GEDARQ_TAMANHO,@GEDARQ_EXTENSAO,@GEDARQ_DESCRICAO,@GEDARQ_LOTE)";

            //            //System.Data.SqlClient.SqlCommand sqlCmdObj = new System.Data.SqlClient.SqlCommand(SqlCmdText, sqlConnection);

            //            //string[] split = strImageName.Split(new string[] { "." }, StringSplitOptions.None);
            //            //string extend = split[split.Length - 1];

            //            //sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELEM", DateTime.Now);
            //            //sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELPOR", login.LOGI_IND);
            //            //sqlCmdObj.Parameters.Add("@GEDARQ_ARQUIVO", System.Data.SqlDbType.Binary, iFileLength).Value = inputBuffer;
            //            //sqlCmdObj.Parameters.AddWithValue("@GEDARQ_TAMANHO", iFileLength);
            //            //sqlCmdObj.Parameters.AddWithValue("@GEDARQ_EXTENSAO", extend);
            //            //sqlCmdObj.Parameters.Add("@GEDARQ_DESCRICAO", System.Data.SqlDbType.VarChar, 255).Value = strImageName.Replace("." + extend, "");
            //            //sqlCmdObj.Parameters.AddWithValue("@GEDARQ_LOTE", Convert.ToInt32(id));

            //            //sqlConnection.Open();
            //            //int i = sqlCmdObj.ExecuteNonQuery();
            //            //sqlConnection.Close();
            //            #endregion
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
            //    string strImageName = "";
            //    try
            //    {
            //        int iFileLength;
            //        HttpFileCollectionBase files = HttpContext.Request.Files;
            //        HttpPostedFileBase uploadfile = files["RemoteFile"];
            //        if (uploadfile != null)
            //        {
            //            strImageName = uploadfile.FileName;
            //            string strFilePath = Server.MapPath(".") + "\\UploadedImages\\Collect";
            //            if (!System.IO.Directory.Exists(strFilePath))
            //            {
            //                System.IO.Directory.CreateDirectory(strFilePath);
            //            }
            //            DateTime now = DateTime.Now;
            //            string strData = now.ToString("yyyyMMdd_HHmmss_") + now.Millisecond + "_" + (new Random().Next() % 1000).ToString();
            //            strFilePath = strFilePath + "\\" + strData + strImageName;
            //            uploadfile.SaveAs(strFilePath);

            //            iFileLength = uploadfile.ContentLength;

            //            Byte[] inputBuffer = new Byte[iFileLength];
            //            System.IO.Stream inputStream;

            //            inputStream = uploadfile.InputStream;
            //            inputStream.Read(inputBuffer, 0, iFileLength);

            //            image = new ImageBuffer();
            //            image.id = id.Value;
            //            image.inputBuffer = inputBuffer;
            //            //image.length = length.Value;

            //            image.strFilePath = strFilePath;
            //            image.listBuffers = new List<byte[]>();
            //            image.listBuffers.Add(inputBuffer);

            //            if (image.length == image.listBuffers.Count())
            //            {

            //                String strConnString;
            //                Model.LoginsInternos login = Session["Usuario"] as Model.LoginsInternos;
            //                DAO.Connection connecion = new DAO.Connection(login.cliente.DATABASE.BASE_SERVER,
            //                                                              login.cliente.DATABASE.BASE_NOMEBANCO,
            //                                                              login.cliente.DATABASE.BASE_USUARIO,
            //                                                              login.cliente.DATABASE.BASE_SENHA);

            //                strConnString = connecion.connectionstring;

            //                System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnString);

            //                String SqlCmdText =
            //                @"insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_LOTE) 
            //                         values (@GEDARQ_DISPONIVELEM,@GEDARQ_DISPONIVELPOR,@GEDARQ_ARQUIVO,@GEDARQ_TAMANHO,@GEDARQ_EXTENSAO,@GEDARQ_DESCRICAO,@GEDARQ_LOTE)";

            //                System.Data.SqlClient.SqlCommand sqlCmdObj = new System.Data.SqlClient.SqlCommand(SqlCmdText, sqlConnection);

            //                sqlConnection.Open();
            //                foreach (byte[] buffer in image.listBuffers)
            //                {
            //                    string[] split = strImageName.Split(new string[] { "." }, StringSplitOptions.None);
            //                    string extend = split[split.Length - 1];

            //                    sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELEM", now);
            //                    sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELPOR", login.LOGI_IND);
            //                    sqlCmdObj.Parameters.Add("@GEDARQ_ARQUIVO", System.Data.SqlDbType.Binary, iFileLength).Value = buffer;
            //                    sqlCmdObj.Parameters.AddWithValue("@GEDARQ_TAMANHO", iFileLength);
            //                    sqlCmdObj.Parameters.AddWithValue("@GEDARQ_EXTENSAO", extend);
            //                    sqlCmdObj.Parameters.Add("@GEDARQ_DESCRICAO", System.Data.SqlDbType.VarChar, 255).Value = strImageName.Replace("." + extend, "");
            //                    sqlCmdObj.Parameters.AddWithValue("@GEDARQ_LOTE", Convert.ToInt32(id));
            //                    int i = sqlCmdObj.ExecuteNonQuery();
            //                }
            //                sqlConnection.Close();
            //            }
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}

            ////return View("Index");
            #endregion
        }

        public int TipoArquivo(int id)
        {
            BD = new ArquivoBD(Session);
            TypesErrors erro = new TypesErrors();
            int tipoArquivo = BD.GetTipoArquivoByArquivo(id, ref erro);
            return tipoArquivo;
        }

        public string SalvarDetail(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic objectJson = serializer.DeserializeObject(json);

            LoginsInternos login = Session["Usuario"] as LoginsInternos;
            DateTime now = DateTime.Now;
            List<Model.Ged.CampoValor> camposValores = new List<Model.Ged.CampoValor>();
            Model.Ged.CampoValor campoValor;
            int idArquivo = 0;
            int plano = 0;
            int conta = 0;
            int tipo = 0;
            foreach (KeyValuePair<string, object> jsonLines in objectJson)
            {
                var jsonLinesKey = jsonLines.Key;
                switch (jsonLinesKey)
                {
                    case "arquivo":
                        idArquivo = Convert.ToInt32(jsonLines.Value);
                        break;
                    case "plano":
                        plano = Convert.ToInt32(jsonLines.Value);
                        break;
                    case "conta":
                        conta = Convert.ToInt32(jsonLines.Value);
                        break;
                    case "tipo":
                        tipo = Convert.ToInt32(jsonLines.Value);
                        break;
                    case "valores":
                        dynamic jsonLine = jsonLines.Value;
                        foreach (Dictionary<string, object> dictionaryLines in jsonLine)
                        {
                            campoValor = new Model.Ged.CampoValor();
                            campoValor.CAPVAL_DATA = now;
                            foreach (KeyValuePair<string, object> keyValueLine in dictionaryLines)
                            {
                                var keyValueLineKey = keyValueLine.Key;
                                switch (keyValueLineKey)
                                {
                                    case "CAPVAL_VALOR":
                                        campoValor.CAPVAL_VALOR = Convert.ToString(keyValueLine.Value);
                                        break;
                                    case "CAMP_IND":
                                        campoValor.CAPVAL_CAMPO = Convert.ToInt32(keyValueLine.Value);
                                        break;
                                }
                            }
                            camposValores.Add(campoValor);
                        }
                        break;
                }
            }

            TypesErrors erro = new TypesErrors();
            BD = new ArquivoBD(Session);
            BD.SetCampoValues(idArquivo, plano, conta, tipo, camposValores, now, login.LOGI_IND, ref erro);

            return Url.Action("Detail/" + idArquivo.ToString(), "Arquivo");
        }

        [HttpPost]
        public PartialViewResult GetContas(int id)
        {
            Thread.Sleep(500);
            BD = new ArquivoBD(Session);
            TypesErrors erro = new TypesErrors();
            PlanoContas plano = new PlanoContas();
            plano.PLAN_IND = id;
            List<Conta> contas = new List<Conta>();
            BD.GetContasByPlano(plano, ref contas, ref erro);
            ClassView.Listas pview = new ClassView.Listas();
            pview.contas = contas;
            return PartialView("Conta/DropDownList", pview);
        }
    }
}