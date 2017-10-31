<%@ Page Language="C#"%>
<%
    string strImageName = "";
    try
    {
        int iFileLength;
        HttpFileCollection files = HttpContext.Current.Request.Files;
        HttpPostedFile uploadfile = files["RemoteFile"];
        if (uploadfile != null)
        {
            strImageName = uploadfile.FileName;
            string strFilePath = Server.MapPath(".") + "\\UploadedImages\\Collect";
            if (!System.IO.Directory.Exists(strFilePath))
            {
                System.IO.Directory.CreateDirectory(strFilePath);
            }
            DateTime now = DateTime.Now;
            string strData = now.ToString("yyyyMMdd_HHmmss_") + now.Millisecond + "_" + (new Random().Next() % 1000).ToString();
            strFilePath = strFilePath + "\\" + strData + strImageName;
            uploadfile.SaveAs(strFilePath);

            iFileLength = uploadfile.ContentLength;

            Byte[] inputBuffer = new Byte[iFileLength];
            System.IO.Stream inputStream;

            inputStream = uploadfile.InputStream;
            inputStream.Read(inputBuffer, 0, iFileLength);

            String strConnString;
            Model.LoginsInternos login = Session["Usuario"] as Model.LoginsInternos;
            DAO.Connection connecion = new DAO.Connection(login.cliente.DATABASE.BASE_SERVER,
                                                          login.cliente.DATABASE.BASE_NOMEBANCO,
                                                          login.cliente.DATABASE.BASE_USUARIO,
                                                          login.cliente.DATABASE.BASE_SENHA);

            strConnString = connecion.connectionstring;

            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnString);

            String SqlCmdText =
            @"insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_CONTARQUIVOTIPO, GEDARQ_LOTE) 
                             values (@GEDARQ_DISPONIVELEM,@GEDARQ_DISPONIVELPOR,@GEDARQ_ARQUIVO,@GEDARQ_TAMANHO,@GEDARQ_EXTENSAO,@GEDARQ_DESCRICAO,  
		                            (select a.CATIP_IND from ContArqTipo a 
		                             inner join GedArquivoTipo c on c.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO 
		                             where c.GEDTIPO_DESCRICAO = 'Boleto'), 1)";

            System.Data.SqlClient.SqlCommand sqlCmdObj = new System.Data.SqlClient.SqlCommand(SqlCmdText, sqlConnection);

            string[] split = strImageName.Split(new string[] { "." }, StringSplitOptions.None);
            string extend = split[split.Length - 1];

            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELEM", DateTime.Now);
            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_DISPONIVELPOR", login.LOGI_IND);
            sqlCmdObj.Parameters.Add("@GEDARQ_ARQUIVO", System.Data.SqlDbType.Binary, iFileLength).Value = inputBuffer;
            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_TAMANHO", iFileLength);
            sqlCmdObj.Parameters.AddWithValue("@GEDARQ_EXTENSAO", extend);
            sqlCmdObj.Parameters.Add("@GEDARQ_DESCRICAO", System.Data.SqlDbType.VarChar, 255).Value = strImageName.Replace(extend, "");

            sqlConnection.Open();
            int i = sqlCmdObj.ExecuteNonQuery();
            sqlConnection.Close();

        }
    }
    catch
    {
    }
%>