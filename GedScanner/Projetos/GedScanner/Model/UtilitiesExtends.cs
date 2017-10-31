using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ImageBuffer
    {
        public int id;
        public byte[] inputBuffer;
        public int length;

        public int iFileLenght;
        public string strFilePath;
        public List<byte[]> listBuffers;
    }

    public class Pagination
    {
        public int rows { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int[] itens { get; set; }
        public int item { get; set; }
        //public string link { get; set; }
        //public string controller { get; set; }
        //public string action { get; set; }

        public Pagination()
        {
            //link = "";
            rows = 0;
            pages = 0;
            page = 0;
            itens = new int[] { 10, 25, 50, 100 };
            item = 0;
        }

        public Pagination(int Rows, int Pages, int Page, int[] Itens, int Item)
        {
            rows = Rows;
            pages = Pages;
            page = Page;
            itens = Itens;
            item = Item;
        }

        //public Pagination(string Action, string Controller, int Rows, int Pages, int Page, int[] Itens, int Item)
        //{
        //    action = Action;
        //    controller = Controller;
        //    rows = Rows;
        //    pages = Pages;
        //    page = Page;
        //    itens = Itens;
        //    item = Item;
        //}
    }
}
