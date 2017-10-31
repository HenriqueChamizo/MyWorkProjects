using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Chamizo.Class;
using System.Data.OleDb;
using System.Data;

namespace Chamizo.Class
{
    public class FileClass
    {
        public TypeFile Type;
        public String Query;

        public void SetTypeFile(TypeFile typeFile)
        {
            Type = typeFile;
        }

        public String[] ReadFile(String Path)
        {
            String[] Lines = File.ReadAllLines(Path, Encoding.Unicode);
            return Lines;
        }

        public void ReadFile(String Path, out String FileFromPath)
        {
            String[] Lines = File.ReadAllLines(Path, Encoding.Unicode);
            String FileLines = "";
            for (int i = 0; i < Lines.Count(); i++)
            {
                FileLines += Lines[i] + "\r\n";
            }
            FileFromPath = FileLines;
        }
    }
}
