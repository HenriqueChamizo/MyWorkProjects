using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFeX;
using AsseFincSharp.DAORelacional;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using AsseFincSharp.FabricaDAORelacional;
using AsseFincSharp.Model;
using AsseFincSharp.Model.NFe;
using System.Globalization;
using System.Windows.Forms;




namespace AsseFincSharp.Negocio
{
    public class rotinasNfe
    {
        private string passo ="";
        public static spdNFeX spdNFe;
        private DAORelacional.DAORelacional dao;
        private DataBaseHelper dHelper;
        public rotinasNfe()
        {
            try
            {
                passo = "dHerlper.spdNFeX";
                spdNFe = new spdNFeX();
                passo = "dHerlper.DataBaseHelper";
                dHelper = new DataBaseHelper();
                passo = "dHerlper.ConectaBanco";
                dao = new DAORelacional.DAORelacional(dHelper.ConectaBanco());

            }
            catch (Exception ex)
            {
                
                throw  new Exception( "Falha na rotina rotinaNFe passo " + passo + "Mensagem :" + ex.Message);
            }

        }
        #region rotinas manifesto_Nfe_e_download_XML
        public string enviaManifestacao(int tipoEvento, string Documento, string idNfe, string Justificativa, string dataEvento, int sequencia, string fuso, string cOrgao)
        {

            string xmlRetorno = "";
            try
            {
                xmlRetorno = spdNFe.EnviarManifestacaoDestinatario(tipoEvento, idNfe, Documento, Justificativa, dataEvento, sequencia, "", cOrgao);
                return xmlRetorno;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public string consultaNFe(string UltimoNSU)
        {
            /*
              aIndNFeIndicador de NF-e consultada. Utilizar: (0 para "Todas as NF-e"; 
            *                                                 1 para "Somente as NF-e sem manifestação";
            *                                                 aIndEmiIndicador do Emissor da NF-e. Utilizar: (0 para "Todos os Emitentes / Remetentes";
            *                                                 aUltNSUÚltimo NSU recebido pela Empresa (Caso seja informado com zero, ou com um NSU muito antigo, aaConfigFilePathcaminho para um arquivo ini com configurações
            */
            string xmlRetorno = "";
            xmlRetorno = spdNFe.ConsultarNFDestinadas(1, 1, UltimoNSU);
            return xmlRetorno;
        }
        public string retornaXmlManifesto(string chave)
        {
            string xmlRetornado;
            chave = util.RetonaChaveNFeLimpa(chave);
            try
            {
                xmlRetornado = spdNFe.DownloadNFe(chave, "91");
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return xmlRetornado;
        }

        #endregion

        public spdNFeX setaPropriedadesSpdNFe(string cnpjParaUsar)
        {
            #region comentado
            /*SqlDataReader Q ;
            string strsql = "SELECT dbo.fn_trim_cnpj2(GRU_CNPJ) AS GRU_CNPJ, GRU_UF, CFG_NFE_AMBIENTE, GRU_CERTIFICADO, CFG_NFE_DIR, CFG_NFE_DIR_LOG FROM Grupos INNER JOIN Config ON CFG_GRUPO = GRU_IND  inner join notas n on (GRU_IND = n.NOT_GRUPO) WHERE N.NOT_IND = " + Convert.ToString(Not_Ind);
            try 
	        {	        
                 SqlParameter parm  = dao.CriaParametro("NOT_IND",System.Data.DbType.Int32,Convert.ToString(Not_Ind));
                  Q = dao.consultar(strsql,dao.addListaParametros(parm));   
	        }
	        catch (Exception ex)
	        {
		
		        throw ex;
	        }
            */
            #endregion

            var dados = dadosGrupo.listaDeGrupos.Where(p => p.GRU_CNPJ == cnpjParaUsar).FirstOrDefault();

            if (dados.CFG_NFE_AMBIENTE == 1)
                spdNFe.Ambiente = Ambiente.akProducao;
            else if (dados.CFG_NFE_AMBIENTE == 2)
                spdNFe.Ambiente = Ambiente.akHomologacao;

            spdNFe.CNPJ = dados.GRU_CNPJ.Trim();
            spdNFe.UF = dados.GRU_UF.Trim();

            try
            {
                spdNFe.NomeCertificado = dados.GRU_CERTIFICADO;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            spdNFe.ArquivoServidoresHom = "nfeServidoresHom.ini";
            spdNFe.ArquivoServidoresProd = "nfeServidoresProd.ini";
            spdNFe.DiretorioXmlDestinatario = dados.CFG_NFE_DIR == "" ? @"\Downloads" : dados.CFG_NFE_DIR;
            spdNFe.DiretorioLog = dados.CFG_NFE_DIR_LOG == "" ? @"\Downloads\log" : dados.CFG_NFE_DIR_LOG;
            spdNFe.DiretorioLogErro = spdNFe.DiretorioLog + "Erros";
            spdNFe.DiretorioTemporario = System.IO.Path.GetTempPath();
            //spdNFe.DiretorioDownloads := GetDirRazaoSocialFormatado(Grupo);
            spdNFe.DiretorioDownloads = @"\Downloads";

            CriaDiretoriosSenaoExistir(spdNFe.DiretorioXmlDestinatario);
            CriaDiretoriosSenaoExistir(spdNFe.DiretorioLog);

            //spdNFe.Usuario := 'teste';
            //spdNFe.Senha := 'teste';
            //spdNFe.Proxy := 'teste';
            spdNFe.TimeOut = 60000;
            spdNFe.MaxSizeLoteEnvio = 500;
            spdNFe.DiretorioEsquemas = @"Esquemas4\";
            spdNFe.DiretorioTemplates = @"Templates4\";
            spdNFe.IgnoreInvalidCertificates = true;
            spdNFe.AnexarDanfePDF = true;
            spdNFe.ValidarEsquemaAntesEnvio = true;
            spdNFe.VersaoManual = "5.0a";

            spdNFe.CaracteresRemoverAcentos = "áéíóúàèìòùâêîôûäëïöüãõñçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÄËÏÖÜÃÕÑÇºª";
            //' DanfeSettings
            spdNFe.FraseContingencia = "Danfe em contingência - Impresso em decorrência de problemas técnicos";
            spdNFe.FraseHomologacao = "SEM VALOR FISCAL";
            spdNFe.QtdeCopias = 1;
            spdNFe.LineDelimiter = "|";
            spdNFe.ModeloRetrato = @"Templates4\Vm50a\Danfe\retrato.rtm";
            spdNFe.ModeloPaisagem = @"Templates4\Vm50a\Danfe\paisagem.rtm";

            return spdNFe;
        }

        private void CriaDiretoriosSenaoExistir(string p)
        {
            if (p.PadRight(1) == @"\")
                p = p.PadLeft(p.Length - 1);

            if (!System.IO.Directory.Exists(p))
            {
                System.IO.Directory.CreateDirectory(p);
            }
        }

        public string mostraDownload()
        {
            return spdNFe.DiretorioDownloads;

        }

        public List<string> CarregaXmlsParaApresentarNagradeDeImportacao(string caminhaDoXml)
        {

            string linhaXml = null;
            string[] Arquivos = null;
            List<string> arrayXML = new List<string>();
            if (System.IO.Directory.Exists(caminhaDoXml))
            {
                // carrego a lista de nomes de arquivos contidos na pasta
                Arquivos = Directory.GetFiles(caminhaDoXml, "*.xml");
            }

            foreach (string arquivo in Arquivos)
            {
                using (StreamReader leitor = new StreamReader(arquivo))
                {

                    linhaXml = leitor.ReadToEnd();
                    linhaXml += "¨" + arquivo;
                    arrayXML.Add(linhaXml);

                    Application.DoEvents(); 
                }
            }

            return arrayXML;

        }

        public void renomeiaArquivoAposImportacao(string caminhaDoXml, string nomeArquivo)
        {

          
            List<string> arrayXML = new List<string>();
            string novoNomeArquivo = "";
            novoNomeArquivo =  nomeArquivo.Replace(".xml", ".okxml");

            if (System.IO.Directory.Exists(caminhaDoXml))
            {

                //if (File.Exists(caminhaDoXml + "\\" + nomeArquivo))
                if (File.Exists(nomeArquivo))
                {
                    // carrego a lista de nomes de arquivos contidos na pasta
                    Directory.Move(nomeArquivo, novoNomeArquivo);
                }
                else
                {
                    novoNomeArquivo = novoNomeArquivo.Replace("ok", "ok" + DateTime.Now.Minute.ToString());
                    // carrego a lista de nomes de arquivos contidos na pasta com o minuto pra nao repitir
                    Directory.Move(nomeArquivo, novoNomeArquivo);
                }
            }
           

        }


        public List<dadosNFeImportada> dadosNFeToImportar(List<string> Lista, string cnpj)
        {
            DAORelacional.DAORelacional dao = new DAORelacional.DAORelacional();
            List<dadosNFeImportada> notas = new List<dadosNFeImportada>();

            foreach (string sXml in Lista)
            {
                string[] ssXml = sXml.Split('¨');
               
                DataSet ds = dao.retornXMLToDataTable(ssXml[0]);

                IdeModel ide = new IdeModel();
                EmitModel emitente = new EmitModel();
                enderEmitModel enderEmit = new enderEmitModel();
                DestModel dest = new DestModel();

                enderDestModel enderDest = new enderDestModel();
                ICMSTotModel total = new ICMSTotModel();
                InfProt prot = new InfProt();
                IList<detModel> itens = new List<detModel>();
                List<Duplicata> cobr = new List<Duplicata>();
                TranspModel transport = new TranspModel();
                InfAdicModel inf = new InfAdicModel();

                ///
                // preenchendo o Ide da nota
                //
                ide = (IdeModel)preencheObjXML(ide, ds.Tables["ide"]);
                //
                // Prenchendo o Emitente da nota
                //
                emitente = (EmitModel)preencheObjXML(emitente, ds.Tables["emit"], ds.Tables["enderEmit"]);
                enderEmit = (enderEmitModel)preencheObjXML(enderEmit, ds.Tables["enderEmit"]);
                emitente.enderEmit = enderEmit;
                //
                // Prenchendo o destinatario da nota
                //
                dest = (DestModel)preencheObjXML(dest, ds.Tables["dest"], ds.Tables["enderDest"]);
                enderDest = (enderDestModel)preencheObjXML(enderDest, ds.Tables["enderDest"]);
                dest.enderDest = enderDest;
                //
                // preenchendo os produtos da nota
                //
                itens = (List<detModel>)preencheObjXML(itens, ds.Tables["prod"], null);
                // falta importar os impostos ... 

                //
                // Preenchendo cobrança
                //
                try { cobr = (List<Duplicata>)preencheObjXML(cobr, ds.Tables["dup"], null); }
                catch { };

                //
                // Preenchendo os dados da transportadora
                //

                try { transport = (TranspModel)preencheObjXML(transport, ds.Tables["transportadora"]); }
                catch { };
                try { transport = (TranspModel)preencheObjXML(transport, ds.Tables["vol"]); }
                catch { };

                try { inf = (InfAdicModel)preencheObjXML(inf, ds.Tables["infAdic"]); }
                catch { };

                total = (ICMSTotModel)preencheObjXML(total, ds.Tables["ICMSTot"]);
                prot = (InfProt)preencheObjXML(prot, ds.Tables["infProt"]);

                dadosNFeImportada nota = new dadosNFeImportada();

                int idFornecedor = 0;
                try
                {
                    idFornecedor = consultaIDViaCNPJ(emitente.CNPJ, emitente.xNome);
                    if (idFornecedor == 0)
                        throw new Exception(" O Fornecedor ( " + nota.Emit.xNome + " )  não esta cadastrado na base de dados.");
                }
                catch (Exception )
                {
                    idFornecedor = 0;
                }

                int idGrupo = 0;
                try
                {
                    idGrupo = consultaIDGrupoViaCNPJ(cnpj, dest.xNome);
                    
                    if (idGrupo == 0)
                        throw new Exception(" O Destnatário ( " + nota.Emit.xNome + " )  não esta cadastrado na base de dados.");
                }
                catch (Exception )
                {
                    idGrupo = 0;
                }


                nota.idFornecedor = idFornecedor;
                nota.idGrupo = idGrupo;
                nota.Ide = ide;
                nota.Emit = emitente;
                nota.Dest = dest;
                nota.Itens = (List<detModel>)itens;
                nota.Cobranca = cobr;
                nota.Transp = transport;
                nota.Total = total;
                nota.InfAdic = inf;
                nota.Prot = prot;

                nota.xmlImportado = sXml;
                nota.nomeArquivo = ssXml[1];
                notas.Add(nota);
            }
            return notas.Where<dadosNFeImportada>(p => p.Dest.CNPJ == cnpj).ToList<dadosNFeImportada>(); ;
        }

        private object preencheObjXML(Object obj, DataTable dt, object SemiPreechido = null)
        {
            object retorno = null;

            if (obj.GetType() == typeof(IdeModel))
                retorno = preencheIDE(dt);
            else if (obj.GetType() == typeof(EmitModel))
                retorno = preencheEMIT(dt);
            else if (obj.GetType() == typeof(enderEmitModel))
                retorno = preencheEnderEmit(dt);
            else if (obj.GetType() == typeof(DestModel))
                retorno = preencheDEST(dt);
            else if (obj.GetType() == typeof(enderDestModel))
                retorno = preencheEnderDest(dt);
            else if (obj.GetType() == typeof(ICMSTotModel))
                retorno = preencheTotal(dt);
            else if (obj.GetType() == typeof(InfProt))
                retorno = preencheProt(dt);
            else if (obj.GetType() == typeof(InfAdicModel))
                retorno = preencheInfAdic(dt);
            else if (obj.GetType() == typeof(List<detModel>))
                retorno = preencheItens(dt);
            else if (obj.GetType() == typeof(List<Duplicata>))
                retorno = preencheCobranca(dt);
            else if (obj.GetType() == typeof(TranspModel))
                if (dt.TableName == "transportadora")
                    retorno = preencheTransportadora(dt);
                else // volume
                    retorno = preencheVolume(dt, (TranspModel)obj);



            return retorno;
        }

        private object preencheInfAdic(DataTable dt)
        {
            InfAdicModel inf = new InfAdicModel();
            inf.infCpl = dt.Rows[0]["infCpl"].ToString();

            return inf;
        }

        private object preencheVolume(DataTable dt, TranspModel transpModel)
        {
            transpModel.vol.esp = dt.Rows[0]["esp"].ToString();
            transpModel.vol.qVol = Convert.ToInt32(dt.Rows[0]["qvol"]);
            transpModel.vol.pesoL = util.formataDecimal(dt.Rows[0]["pesoL"].ToString());
            transpModel.vol.pesoB = util.formataDecimal(dt.Rows[0]["pesoB"].ToString());

            return transpModel;
        }

        private object preencheTransportadora(DataTable dt)
        {
            TranspModel trans = new TranspModel();

            trans.transportadora.CNPJ = dt.Rows[0]["CNPJ"].ToString();
            trans.transportadora.IE = dt.Rows[0]["IE"].ToString();
            trans.transportadora.UF = dt.Rows[0]["UF"].ToString();
            trans.transportadora.xEnder = dt.Rows[0]["xEnder"].ToString();
            trans.transportadora.xMun = dt.Rows[0]["xMun"].ToString();
            trans.transportadora.xNome = dt.Rows[0]["xNome"].ToString();

            return trans;
        }

        private object preencheCobranca(DataTable dt)
        {
            List<Duplicata> cbr = new List<Duplicata>();
            Duplicata dup = null;
            double valorAcumulo = 0.00;

            int i =0;
            foreach (DataRow dr in dt.Rows)
            {
                valorAcumulo += Convert.ToDouble(util.formataDecimal(dr["vdup"].ToString()));
            }

            foreach (DataRow dr in dt.Rows)
            {
                dup = new Duplicata();
                dup.Fatura = dr["ndup"].ToString();
                dup.Vencimento = Convert.ToDateTime(util.formatDataEhora(dr["dVenc"].ToString()));
                dup.Valor = util.formataDecimal(dr["vdup"].ToString());
                dup.valor_totalDuplicata = valorAcumulo;
                cbr.Add(dup);
            }

            

            return cbr;
        }

        private List<detModel> preencheItens(DataTable dt)
        {
            List<detModel> ListItens = new List<detModel>();
            detModel item = null;

            foreach (DataRow dr in dt.Rows)
            {
                item = new detModel();
                item.prod.xProd = dr["xProd"].ToString();
                item.prod.cProd = dr["cProd"].ToString();
                try { item.prod.cEAN = dr["cEAN"].ToString(); }
                catch { }
                item.prod.NCM = dr["NCM"].ToString();
                item.prod.CFOP = Convert.ToInt32(dr["CFOP"]);
                item.prod.uCom = dr["uCom"].ToString();
                item.prod.qCom = util.formataDecimal(dr["qCom"].ToString());
                item.prod.vUnCom = util.formataDecimal(dr["vunCom"].ToString());
                item.prod.vProd = util.formataDecimal(dr["vProd"].ToString());
                try { item.prod.cEANTrib = dr["cEANTrib"].ToString(); }
                catch { }
                item.prod.uTrib = dr["UTrib"].ToString();
                item.prod.qTrib = util.formataDecimal(dr["qTrib"].ToString());
                item.prod.vUnTrib = util.formataDecimal(dr["vUnTrib"].ToString());
                try { item.prod.vFrete = util.formataDecimal(dr["vFrete"].ToString()); }
                catch { }
                item.prod.indTot = Convert.ToInt32(dr["indTot"]);
                item.prod.vOutros = item.prod.vFrete;
                item.prod.vTotal = util.formataDecimal((item.prod.vProd + item.prod.vOutros).ToString());
                ListItens.Add(item);
                item = new detModel();
            }

            return ListItens;
        }

        private object preencheProt(DataTable dt)
        {

            InfProt inf = new InfProt();
            inf.chNFe = dt.Rows[0]["chNFe"].ToString();
            inf.cStat = Convert.ToInt32(dt.Rows[0]["cStat"]);
            inf.dhRecbto = util.formatDataEhora(dt.Rows[0]["dhRecbto"].ToString());
            inf.digVal = dt.Rows[0]["digVal"].ToString();
            inf.nProt = Convert.ToInt64(dt.Rows[0]["nProt"]);
            inf.tpAmb = Convert.ToInt32(dt.Rows[0]["tpAmb"]);
            inf.verAplic = dt.Rows[0]["verAplic"].ToString();
            inf.xMotivo = dt.Rows[0]["xMotivo"].ToString();
            return inf;

        }

        private object preencheTotal(DataTable dt)
        {
            ICMSTotModel total = new ICMSTotModel();
            total.vBC = util.formataDecimal(dt.Rows[0]["vBC"].ToString() );
            total.vBCST = util.formataDecimal(dt.Rows[0]["vBCST"].ToString());
            total.vCOFINS = util.formataDecimal(dt.Rows[0]["vCOFINS"].ToString());
            total.vDesc = util.formataDecimal(dt.Rows[0]["vDesc"].ToString());
            try { total.vFCPUFDest = util.formataDecimal(dt.Rows[0]["vFCPUFDest"].ToString()); }
            catch { }
            total.vFrete = util.formataDecimal(dt.Rows[0]["vFrete"].ToString());
            total.vICMS = util.formataDecimal(dt.Rows[0]["vICMS"].ToString());

            try { total.vICMSDeson = util.formataDecimal(dt.Rows[0]["vICMSDeson"].ToString()); }
            catch { };
            try { total.vICMSUFDest = util.formataDecimal(dt.Rows[0]["vICMSUFDest"].ToString()); }
            catch { };
            try { total.vICMSUFRemet = util.formataDecimal(dt.Rows[0]["vICMSUFRemet"].ToString()); }
            catch { };
            total.vII = util.formataDecimal(dt.Rows[0]["vII"].ToString());
            total.vIPI = util.formataDecimal(dt.Rows[0]["vIPI"].ToString());
            total.vNF = util.formataDecimal(dt.Rows[0]["vNF"].ToString());
            total.vOutro = util.formataDecimal(dt.Rows[0]["vOutro"].ToString());
            total.vPIS = util.formataDecimal(dt.Rows[0]["vPIS"].ToString());
            total.vProd = util.formataDecimal(dt.Rows[0]["vProd"].ToString());
            total.vSeg =util.formataDecimal(dt.Rows[0]["vSeg"].ToString() );
            total.vST = Convert.ToDouble(dt.Rows[0]["vST"].ToString());

            return total;
        }

        private object preencheEMIT(DataTable dt)
        {
            EmitModel emit = new EmitModel();
            
            
            if (dt.Columns.Contains("CNAE") )
            emit.CNAE = dt.Rows[0]["CNAE"].ToString();
            
            emit.CNPJ = dt.Rows[0]["CNPJ"].ToString();
            emit.CRT = Convert.ToInt32(dt.Rows[0]["CRT"]);

            if (dt.Columns.Contains("XFant"))
            emit.xFant = dt.Rows[0]["XFant"].ToString();

            emit.xNome = dt.Rows[0]["XNome"].ToString();


            return emit;
        }

        private object preencheDEST(DataTable dt)
        {
            DestModel dest = new DestModel();
            dest.CNPJ = dt.Rows[0]["CNPJ"].ToString();

            if (dt.Columns.Contains("XNome"))
            dest.xNome = dt.Rows[0]["XNome"].ToString();

            if (dt.Columns.Contains("IM"))
            dest.IM = dt.Rows[0]["IM"].ToString();

            if (dt.Columns.Contains("indIEDEst"))
            dest.indIEDest = Convert.ToInt32(dt.Rows[0]["indIEDEst"].ToString());

            return dest;
        }

        private object preencheEnderEmit(DataTable dt)
        {
            enderEmitModel ender = new enderEmitModel();



            ender = new enderEmitModel();
            ender.CEP = dt.Rows[0]["Cep"].ToString();
            ender.cMun = dt.Rows[0]["cMun"].ToString();
            if (dt.Columns.Contains("cPais"))
             ender.cPais = Convert.ToInt32(dt.Rows[0]["cPais"]); 

            ender.nro = dt.Rows[0]["nro"].ToString();
            ender.UF = dt.Rows[0]["UF"].ToString();
            ender.xBairro = dt.Rows[0]["xBairro"].ToString();
            
            if (dt.Columns.Contains("xCpl"))
                ender.xCpl = dt.Rows[0]["xCpl"].ToString();
            
            ender.xLgr = dt.Rows[0]["xLgr"].ToString();
            ender.xMun = dt.Rows[0]["xMun"].ToString();

            if (dt.Columns.Contains("xPais"))
            ender.xPais = dt.Rows[0]["xPais"].ToString();

            if (dt.Columns.Contains("fone"))
            ender.Fone = dt.Rows[0]["fone"].ToString(); 

            return ender;
        }
        private object preencheEnderDest(DataTable dt)
        {
            enderDestModel ender = new enderDestModel();


            ender = new enderDestModel();
            ender.CEP = dt.Rows[0]["Cep"].ToString();
            ender.cMun = dt.Rows[0]["cMun"].ToString();
            
            ender.nro = dt.Rows[0]["nro"].ToString();
            ender.UF = dt.Rows[0]["UF"].ToString();
            ender.xBairro = dt.Rows[0]["xBairro"].ToString();

            if (dt.Columns.Contains("xCpl"))
            ender.xCpl = dt.Rows[0]["xCpl"].ToString();
           
            ender.xLgr = dt.Rows[0]["xLgr"].ToString();
            ender.xMun = dt.Rows[0]["xMun"].ToString();

            if (dt.Columns.Contains("xPais"))
            ender.xPais = dt.Rows[0]["xPais"].ToString();

            if (dt.Columns.Contains("cPais"))
            ender.cPais = Convert.ToInt32(dt.Rows[0]["cPais"].ToString());
            

            return ender;
        }

        private object preencheIDE(DataTable dt)
        {
            IdeModel ide = new IdeModel();
            ide.cDV = Convert.ToInt32(dt.Rows[0]["cDV"]);
            ide.cMunFG = Convert.ToInt32(dt.Rows[0]["cMunFG"]);
            ide.cNF = Convert.ToInt32(dt.Rows[0]["cNF"]);
            ide.cUF = dt.Rows[0]["cUF"].ToString();
            ide.dhEmi = util.formatDataEhora(dt.Rows[0]["dhEmi"].ToString());
            
            if (dt.Columns.Contains("dhSaiEnt"))
            ide.dhSaiEnt = util.formatDataEhora(dt.Rows[0]["dhSaiEnt"].ToString());
            
            ide.finNFe = Convert.ToInt32(dt.Rows[0]["finNFe"]);
            ide.indFinal = Convert.ToInt32(dt.Rows[0]["indFinal"]);
            ide.indPag = Convert.ToInt32(dt.Rows[0]["indPag"]);
            ide.indPres = Convert.ToInt32(dt.Rows[0]["indPres"]);
            ide.mod = Convert.ToInt32(dt.Rows[0]["mod"]);
            ide.natOp = dt.Rows[0]["natOp"].ToString();
            ide.nNf = Convert.ToInt32(dt.Rows[0]["nNF"]);
            ide.procEmi = Convert.ToInt32(dt.Rows[0]["ProcEmi"]);
            ide.serie = Convert.ToInt32(dt.Rows[0]["serie"]);
            ide.tpAmb = Convert.ToInt32(dt.Rows[0]["tpAmb"]);
            ide.tpEmis = Convert.ToInt32(dt.Rows[0]["tpEmis"]);
            ide.tpImp = Convert.ToInt32(dt.Rows[0]["tpImp"]);
            ide.tpNF = Convert.ToInt32(dt.Rows[0]["tpNF"]);
            ide.veProc = dt.Rows[0]["tpAmb"].ToString();

            return ide;
        }

        public List<dadosApresentacaoNotaImportar> defineCabecalhoNF(List<dadosNFeImportada> notas)
        {
            List<dadosApresentacaoNotaImportar> cabecalhos = new List<dadosApresentacaoNotaImportar>();
            dadosApresentacaoNotaImportar cabecalho = new dadosApresentacaoNotaImportar();
            foreach (dadosNFeImportada nf in notas)
            {
                cabecalho.Nota = nf.Ide.nNf;
                cabecalho.dhEmissao = nf.Ide.dhEmi;
                cabecalho.dtAltorizacao = nf.Prot.dhRecbto;
                cabecalho.Emitente = nf.Emit.xNome;
                cabecalho.nProtocolo = nf.Prot.nProt.ToString();
                cabecalho.dtSaida = nf.Ide.dhSaiEnt;
                //cabecalho.tpMovito = "E";
                cabecalho.Valor = nf.Total.vNF;

                cabecalhos.Add(cabecalho);
                cabecalho = new dadosApresentacaoNotaImportar();
            }

            return cabecalhos;
        }

        public ListNatOper consultaNaturesaOperacao()
        {
            ConsultasDAO daonatOper = new DAORelacional.ConsultasDAO(dao);

            ListNatOper listaN = new ListNatOper();

            listaN = daonatOper.consultaNaturesaOperacao();
            return listaN;
        }

        public int consultaIDViaCNPJ(string cnpj, string nome)
        {
            ConsultasDAO daonatOper = new DAORelacional.ConsultasDAO(dao);
            return daonatOper.ConsultaIdFornecedorViaCNPJ(cnpj, nome);

        }

        private int consultaIDGrupoViaCNPJ(string cnpj, string nome)
        {
            ConsultasDAO daoIdGrupo = new DAORelacional.ConsultasDAO(dao);
            return daoIdGrupo.ConsultaIDGrupoViaCNPJ(cnpj, nome); 
        }

        public List<prodModel> carregaListadeProdutosDaNota(List<detModel> list)
        {
            List<prodModel> lstProd = new List<prodModel>();
            prodModel prod;
            foreach (detModel det in list)
            {
                prod = new prodModel();
                prod.xProd = det.prod.xProd;

                try { prod.cEAN = det.prod.cEAN; }
                catch { }
                if (util.isNumeric(prod.cEAN))
                    prod.cProd = det.prod.cEAN;
                else
                    prod.cProd = det.prod.cProd
                ;
                prod.NCM = det.prod.NCM;

                prod.CFOP = det.prod.CFOP;
                prod.uCom = det.prod.uCom; ;
                prod.qCom = det.prod.qCom;
                prod.vUnCom = det.prod.vUnCom;
                prod.vProd = det.prod.vProd;
                prod.cEANTrib = det.prod.cEANTrib;
                prod.uTrib = det.prod.uTrib;
                prod.qTrib = det.prod.qTrib;
                prod.vUnTrib = det.prod.vUnCom;
                prod.vFrete = det.prod.vFrete;
                prod.vOutros = (det.prod.vFrete); // somar todos o valores acessórios
                prod.vTotal = (det.prod.vProd + det.prod.vFrete);


                prod.indTot = det.prod.indTot;

                lstProd.Add(prod);
            }

            return lstProd;
        }
        public ListaDeXPara carregaListaDexPara(dadosNFeImportada nota)
        {

            dadosDeParaModel item = new dadosDeParaModel();
            ListaDeXPara lista = new ListaDeXPara();

            ConsultasDAO cDao = new DAORelacional.ConsultasDAO(dao);
            string sListaProdutos = "'";
            string idFornecedor = "";

            foreach (detModel prod in nota.Itens)
            {
                item.codigoNota = prod.prod.cProd;
                item.descricaoProduto = prod.prod.xProd;
                try { item.FornecedorInd = nota.idFornecedor; }
                catch { }
                lista.ListaDePara.Add(item);

                idFornecedor = item.FornecedorInd.ToString();
                sListaProdutos += item.codigoNota.ToString() + "','";
                

                item = new dadosDeParaModel();
            }
             if (sListaProdutos.Length > 0)
              sListaProdutos = sListaProdutos.Substring(0, sListaProdutos.Length - 2);

             SqlDataReader dr =  cDao.consultaProdutosVinculados(sListaProdutos, idFornecedor);
             while (dr.Read())
             {
               dadosDeParaModel reg = (dadosDeParaModel)  lista.ListaDePara.Find(p=>p.codigoNota.Equals(dr["PRODFOR_INDPRODFOR"].ToString()));
               reg.produtoInd = Convert.ToInt32(dr["PRODFOR_INDPRO"]);
               reg.Nome_Catalogo = dr["PRODFOR_dsCatalogo"] == null ? "" : dr["PRODFOR_dsCatalogo"].ToString() ;
               reg.ClaInd =  Convert.ToInt32(dr["CLA_IND"]);
               reg.dsClassicacao = dr["CLA_DESCRICAO"] == null ? "" : dr["CLA_DESCRICAO"].ToString();
             }

            return lista;

        }



        public List<Duplicata> desmembraParcelas(List<Duplicata> list, int quatasParcelas)
        {
            double total = 0;

            double novaParcela = 0;
            string primeiraFatura = list[0].Fatura;
            string baseFatura = primeiraFatura.Substring(0, primeiraFatura.Length - 2);
            string fatura = "";
            DateTime dataVencimento = list[0].Vencimento;
            Duplicata xdup;
            foreach (Duplicata dup in list)
            {
                total += dup.Valor;
            }

            list.RemoveAll(item => item.Valor > 0 || item.Fatura == null);

            novaParcela = (total / quatasParcelas);
            for (int i = 0; i < quatasParcelas; i++)
            {
                fatura = baseFatura + "/" + Convert.ToString(i + 1);
                xdup = new Duplicata();
                xdup.Fatura = fatura;
                xdup.Vencimento = dataVencimento;
                xdup.Valor = util.formataDecimal(novaParcela.ToString());
                list.Add(xdup);
            }
            return list;


        }

        public List<Duplicata> reajustaDatasVencimento(List<Duplicata> list, string Periodo, DateTime dataInicial)
        {

            for (int passo = 1; passo < list.Count; passo++)
            {
                list[passo].Vencimento = util.SomaPeriodoNadata(Periodo, dataInicial, passo);

            }

            return list;
        }

        public List<SqlParameter> preparaParametrosPesquisa(string tipo, List<string> parm)
        {
            filtro fil = new filtro();
            List<SqlParameter> parametros = new List<SqlParameter>();
            int iAux = 0;
            if (tipo == "movimentacaopesqE" || tipo == "catalogo")
                    {
                        /// pesquisa no catalogo de produtos
                
                        foreach (string str in parm)
                        {
                            if (util.isNumeric(str) && iAux ==0  )
                            {
                                fil.nomeParametro = "@GRUPO";
                                fil.tipo = SqlDbType.Int;
                                fil.valor = str;
                            }
                            else if (iAux > 0 && util.isNumeric(str))
                            {
                                fil.nomeParametro = "@Produto";
                                fil.tipo = SqlDbType.Int;
                                fil.valor = str;

                            }
                            else if (!util.isNumeric(str))
                            {
                                fil.nomeParametro = "@DESCRICAO";
                                fil.tipo = SqlDbType.NVarChar;
                                fil.valor =  str ;
                            }
                           
                            parametros =  dao.retornaListadeParametros(fil, ref parametros);
                            iAux += 1;   
                        }
                        
                    }
            else if (tipo == "movimentacaopesqS")
            {
                 iAux = 0;
                foreach (string str in parm)
                {
                    if (util.isNumeric(str))
                    {

                        if (iAux ==0)
                        {
                            // E codigo de empresa
                            fil.nomeParametro = "@GRUPO";
                            fil.tipo = SqlDbType.Int;
                            fil.valor = str;
                        }
                        else
                        {
                            // e codigo de produto
                            fil.nomeParametro = "@Produto";
                            fil.tipo = SqlDbType.Int;
                            fil.valor = str;

                        }
                    }
                    else
                    {
                        fil.nomeParametro = "@DESCRICAO";
                        fil.tipo = SqlDbType.NVarChar;
                        fil.valor = str;
                    }



                    parametros = dao.retornaListadeParametros(fil, ref parametros);
                    iAux += 1;
                }
            }
            

            return parametros;
        }

    
        public DataTable consultaProdutosCatalogo(List<SqlParameter> parametros)
        {
            string strsql = "select PRO_IND,UPPER(PRO_DESCRICAO) AS 'PRO_DESCRICAO',PRO_PRODUTO_GRUPO,";
            strsql += " ISNULL(PROG_DESCRICAO,'') AS 'PROG_DESCRICAO',c.CLA_IND, c.CLA_DESCRICAO  from produtos AS a LEFT OUTER JOIN ";
                         strsql += " ProdutosGrupos AS b ON a.PRO_PRODUTO_GRUPO = b.PROG_IND ";
                         strsql += "left join Classificacoes c on (a.PRO_CLASSIFICACAO_CTPG = c.CLA_IND) ";
                         strsql += " where pro_grupo=@Grupo ";
            string strcompl = "";
            int iaux = 0;
            foreach (SqlParameter parm in parametros)
            {

                if (parm.SqlDbType == SqlDbType.NVarChar)
                {
                    parm.Size = 300;
                    strcompl = " and  PRO_DESCRICAO LIKE '%" + parm.Value +  "%'";
                  
                }
                else if (parm.SqlDbType == SqlDbType.Int && iaux > 0)
                    strcompl += " and  ltrim(rtrim(pro_codigo)) like '" + parm.Value + "%'";
                iaux += 1;
            }

            strsql += strcompl;

            try
            {
                
                //parametros = null;
                DataTable dt = dao.consultarComRetornoDT(strsql + strcompl, parametros);
                DataRow drow = dt.NewRow();
                drow["PRO_IND"] = "0";
                drow["PRO_DESCRICAO"] = "Selecione ...";
                dt.Rows.InsertAt(drow, 0);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public bool validaDePara(ListaDeXPara lista, bool p)
        {
            bool todosOsProdutosTemCorrelacao = false;
            foreach (dadosDeParaModel dep in lista.ListaDePara){
                todosOsProdutosTemCorrelacao = (dep.produtoInd > 0);
                if (!todosOsProdutosTemCorrelacao)
                    break;
            }

           return todosOsProdutosTemCorrelacao;
        }

        public bool validaClassificacaoProduto(ListaDeXPara lista, bool p)
        {
            bool todosOsProdutosTemClassificacao = false;
            foreach (dadosDeParaModel dep in lista.ListaDePara)
            {
                todosOsProdutosTemClassificacao = (dep.ClaInd > 0);
                if (!todosOsProdutosTemClassificacao)
                    break;
            }

            return todosOsProdutosTemClassificacao;
        }

        internal void adicionaCFOPNaLista(string key, string valor)
        {
            CfopModel obj = new CfopModel();
            obj.cfop = key.Replace("_", "");
            obj.descricao = valor;
            ListaCFOP.cfops.Add(obj);
        }

        internal void adicionaListadeRecurso(string key, string valor, ref List<cstGenericoModel> lista)
        {
                cstGenericoModel obj = new cstGenericoModel();
                obj.codigo = key.Replace("_", "");
                obj.Descricao = valor;
                lista.Add(obj);
        }

        public bool validaCruzamentodeComprasComaNota(string vlfalta)
        {
            double faltas = Convert.ToDouble(vlfalta);
            return faltas != 0;
        }

        public void cadastraNaturecaoOperacaoSenaoExistir(string natureza)
        {
           string  strsql = "select count(*) as existe from NaturezasOp where nat_descricao = '" + natureza.ToUpper() + "'";
           int exist = 0; 
           try
           {
                DataTable dt = dao.consultarComRetornoDT(strsql,null);
                exist = Convert.ToInt32(dt.Rows[0]["existe"]);
                if (exist == 0)
                {
                    strsql = "insert into NaturezasOp ( NAT_GRUPO, NAT_DESCRICAO) values (1,'" + natureza.ToUpper() + "')";

                    try
                    {
                        dao.executaComando(strsql, null);
                    }
                    catch (Exception ex)
                    {
                        
                        throw new Exception("Rotina RotinasNFe / cadastraNaturecaoOperacaoSenaoExistir : " + (char)13 + " Mensagem : " + ex.Message )  ;
                    }
                  
                }
           }
           catch (Exception ex)
           {

               throw new Exception("Rotina RotinasNFe / ConsultaNaturezasOp : " + (char)13 + " Mensagem : " + ex.Message);
           }
        }
    }
}
