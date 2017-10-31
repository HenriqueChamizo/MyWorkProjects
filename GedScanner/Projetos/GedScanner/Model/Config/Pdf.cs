using Persits.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Config
{
    public class Pdf
    {
        public PdfManager pdfmanager;

        public Pdf()
        {
            pdfmanager = new PdfManager();
            pdfmanager.RegKey = "GYlbNz0Jtvp2SEXhSunif/sB0syzFWhpNtzZhQ78lRYILMPnIFKBEEdtaXczQMlh0DNjAyG/ZXe5";
        }
    }
}
