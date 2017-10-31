using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chamizo.Class;

namespace Chamizo.Control
{
    public partial class FileControl : UserControl
    {
        public FileControl()
        {
            InitializeComponent();
        }

        //Abre arquivo e retorna seu nome e caminho. Pode ser Txt ou Excel
        public String OpenFile(TypeFile TypeFilePath, String Path = "C:\\")
        {
            OpenFileDialog open = new OpenFileDialog();
            String FileSource = "";

            open.InitialDirectory = Path;
            if (TypeFilePath == TypeFile.Txt)
            {
                open.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                open.FilterIndex = 2;
            }
            else if (TypeFilePath == TypeFile.Excel)
            {
                open.Filter = "Excel 2012 files (*.xlsx)|*.xlsx|Excel 2007 files (*.xls)|*.xls|All files (*.*)|*.*";
                open.FilterIndex = 2;
            }
            else
            {
                open.Filter = "All files (*.*)|*.*";
                open.FilterIndex = 1;
            }
            open.RestoreDirectory = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                FileSource = open.FileName;

                if (String.IsNullOrEmpty(FileSource))
                {
                    MessageBox.Show("Arquivo Invalido", "Salvar Como", MessageBoxButtons.OK);
                    FileSource = "-1";
                }
            }

            return FileSource;
        }

        public String OpenFolder(Environment.SpecialFolder SpecialFolderOpen = Environment.SpecialFolder.Desktop)
        {
            String FileDestination = "";
            try
            {
                FolderBrowserDialog open = new FolderBrowserDialog();
                open.RootFolder = SpecialFolderOpen;

                while ((open.ShowDialog() == DialogResult.Cancel))
                {
                    MessageBox.Show("Por favor, selecione um local onde será salvo o arquivo.");
                }
                FormatData formatData = new FormatData();
                FileDestination = open.SelectedPath;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                FileDestination = "-1";
            }

            return FileDestination;
        }
    }
}
