using Model.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GedScannerMVC.ClassView
{
    public class BoxSolid
    {
        public TypesBootstrapColors box_type_class;
        public string box_title;
        public TableOfWeb table;
        public bool isCollapsed;
        public Model.Pagination pagination;
        public TableDataType typeTable;
        private TableData tabledata;
        public string ItensString;
        public bool isSubBox = false;

        //public BoxSolid(TableDataType typetable, List<object> objs)
        //{
        //    typeTable = typetable;

            
        //    int rowsp = objs.Count;
        //    int[] itens = new int[] { 10, 25, 50, 100 };
        //    int item = 0;
        //    double dpages = Convert.ToDouble(rowsp) / Convert.ToDouble(itens[item]);
        //    int ipages = rowsp / itens[item];
        //    int pages = dpages > ipages ? ipages + 1 : ipages;
        //    int page = 1;

        //    pagination = new Model.Pagination(typetable.ToString(), rowsp, pages, page, itens, item);
            
        //    tabledata = new TableData(typetable);
        //    table = tabledata.GetDataTable(objs.GetRange(0, 10));

        //    ItensString = "";
        //    for (int i = 0; i < itens.Length; i++)
        //    {
        //        ItensString += "-" + itens[i];
        //    }
        //}

        //public BoxSolid(HttpSessionStateBase Session, TableDataType typetable, Model.Pagination pag)
        //{
        //    typeTable = typetable;
        //    pagination = pag;
        //    ItensString = "";
        //    for (int i = 0; i < pagination.itens.Length; i++)
        //    {
        //        ItensString += "-" + pagination.itens[i];
        //    }

        //    switch (typeTable)
        //    {
        //        case TableDataType.PlanoContas:
        //            ClassBD.PlanoContasBD bd = new ClassBD.PlanoContasBD(Session);
        //            List<Model.PlanoContas> planos = new List<Model.PlanoContas>();
        //            tabledata = new TableData(typetable);
        //            TypesErrors erro = new TypesErrors();
        //            if (bd.GetPlanoContas(ref planos, ref pagination, ref erro))
        //            {
        //                List<object> objs = new List<object>();
        //                foreach (Model.PlanoContas plano in planos)
        //                {
        //                    objs.Add(plano);
        //                }
        //                table = tabledata.GetDataTable(objs);
        //            }
        //            else
        //                table = new TableOfWeb(new List<TableColunas>() { new TableColunas() { Text = "Erro" } }, 
        //                                       new List<TableLinhas>() { new TableLinhas() {
        //                                           cells = new List<TableCelulas>() { new TableCelulas() { Value = "Erro ao trazer valores" } } } }, "");
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}