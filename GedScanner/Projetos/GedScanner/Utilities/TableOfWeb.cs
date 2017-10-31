using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Utilities
{
    public class TableColunas : HtmlGenericControl
    {
        public int tabindex;
        public string aria_controls;
        public int rowspan;
        public int colspan;
        public string aria_sort;
        public string aria_label;
        public string Text;
    }

    public class TableLinhas : HtmlGenericControl
    {
        public string CssClass;

        public List<TableCelulas> cells;
    }

    public class TableCelulas : HtmlGenericControl
    {
        public string Value;
        public string CssClass;
    }

    public class TableOfWeb
    {
        public string LeftTitle;
        public string LeftValue;

        public string RightTitle;
        public string RightValue;

        public string Empty = "Sem registros!";

        private List<TableColunas> columns;
        private List<TableLinhas> rows;

        private Panel fatherPanel;
        private UpdatePanel fatherUpdate;

        public TableOfWeb(List<TableColunas> colunas, List<TableLinhas> linhas, Panel pai)
        {
            columns = colunas;
            rows = linhas;
            fatherPanel = pai;
        }

        public TableOfWeb(List<TableColunas> colunas, List<TableLinhas> linhas, UpdatePanel pai)
        {
            columns = colunas;
            rows = linhas;
            fatherUpdate = pai;
        }

        public void CreateTable(ref Panel pai)
        {
            HtmlGenericControl row_div = new HtmlGenericControl("div");
            row_div.Attributes.Add("class", "row");

            HtmlGenericControl br = new HtmlGenericControl("div");
            br.Attributes.Add("class", "col-md-12");
            br.InnerHtml = "<br />";

            row_div.Controls.Add(br);

            //<input class="btn btn-default" type="button" value="Input">

            HtmlGenericControl panel = new HtmlGenericControl("div");
            panel.Attributes.Add("class", "col-md-12");

            HtmlGenericControl panel_warning = new HtmlGenericControl("div");
            panel_warning.Attributes.Add("class", "panel-hcc panel-hcc-default");

            HtmlGenericControl panel_heading = new HtmlGenericControl("div");
            panel_heading.Attributes.Add("class", "panel-hcc-heading");

            HtmlGenericControl panel_ajust = new HtmlGenericControl("div");
            panel_ajust.Attributes.Add("class", "panel-hcc-ajust");

            HtmlGenericControl left = new HtmlGenericControl("div");
            left.Attributes.Add("class", "left");
            if (!string.IsNullOrEmpty(LeftTitle) && string.IsNullOrEmpty(LeftValue))
                left.InnerHtml = "<b>" + LeftTitle + "</b>";
            else if (!string.IsNullOrEmpty(LeftTitle) && !string.IsNullOrEmpty(LeftValue))
                left.InnerHtml = "<b>" + LeftTitle + ": </b>" + LeftValue;

            HtmlGenericControl right = new HtmlGenericControl("div");
            right.Attributes.Add("class", "right");
            if (!string.IsNullOrEmpty(RightTitle) && string.IsNullOrEmpty(RightValue))
                right.InnerHtml = "<b>" + RightTitle + "</b>";
            else if (!string.IsNullOrEmpty(RightTitle) && !string.IsNullOrEmpty(RightValue))
                right.InnerHtml = "<b>" + RightTitle + ": </b>" + RightValue;

            HtmlGenericControl panel_body = new HtmlGenericControl("div");
            panel_body.Attributes.Add("class", "panel-hcc-body");

            HtmlGenericControl table = new HtmlGenericControl("table");
            table.Attributes.Add("class", "table-hcc table-hcc-striped table-hcc-bordered table-hcc-hover dataTable no-footer dtr-inline");
            table.Attributes.Add("role", "grid");
            table.Attributes.Add("aria-describedby", "dataTables");
            table.Style.Add(HtmlTextWriterStyle.Width, "100%");

            HtmlGenericControl role_row = new HtmlGenericControl("tr");
            role_row.Attributes.Add("role", "row");

            HtmlGenericControl sorting;
            foreach (TableColunas column in columns)
            {
                sorting = new HtmlGenericControl("th");
                sorting.Attributes.Add("tabindex", column.tabindex.ToString());
                sorting.Attributes.Add("aria-controls", column.aria_controls);
                sorting.Attributes.Add("rowspan", column.rowspan.ToString());
                sorting.Attributes.Add("colspan", column.colspan.ToString());
                sorting.Attributes.Add("aria-sort", column.aria_sort);
                sorting.Attributes.Add("aria-label", column.aria_label);
                sorting.InnerText = column.Text;
                sorting.Style.Value = column.Style.Value;
                role_row.Controls.Add(sorting);
            }

            HtmlGenericControl thead = new HtmlGenericControl("thead");
            thead.Controls.Add(role_row);

            HtmlGenericControl tbody = new HtmlGenericControl("tbody");

            HtmlGenericControl tr_odd = new HtmlGenericControl("tr");
            HtmlGenericControl tr_even;

            HtmlGenericControl td = new HtmlGenericControl("td");
            if (rows.Count > 0)
            {
                bool even = false;
                foreach (TableLinhas row in rows)
                {
                    if (!even)
                    {
                        tr_odd = new HtmlGenericControl("tr");
                        tr_odd.Attributes.Add("class", "gradeA odd");
                        tr_odd.Attributes.Add("role", "row");

                        foreach (TableCelulas cell in row.cells)
                        {
                            td = new HtmlGenericControl("td");
                            td.Attributes.Add("class", "sorting_1");
                            td.InnerText = cell.Value;
                            td.Style.Value = cell.Style.Value;
                            tr_odd.Controls.Add(td);
                        }

                        tbody.Controls.Add(tr_odd);
                        even = true;
                    }
                    else
                    {
                        tr_even = new HtmlGenericControl("tr");
                        tr_even.Attributes.Add("class", "gradeA even");
                        tr_even.Attributes.Add("role", "row");

                        foreach (TableCelulas cell in row.cells)
                        {
                            td = new HtmlGenericControl("td");
                            td.Attributes.Add("class", "sorting_1");
                            td.InnerText = cell.Value;
                            td.Style.Value = cell.Style.Value;
                            tr_even.Controls.Add(td);
                        }

                        tbody.Controls.Add(tr_even);
                        even = false;
                    }
                }
            }
            else
            {
                td = new HtmlGenericControl("td");
                td.Attributes.Add("class", "sorting_1");
                td.Style.Add("text-align", "center");
                td.InnerText = Empty;
                td.Attributes.Add("colspan", "7");
                tr_odd.Controls.Add(td);

                tbody.Controls.Add(tr_odd);
            }

            table.Controls.Add(thead);
            table.Controls.Add(tbody);

            panel_body.Controls.Add(table);

            HtmlGenericControl panel_colmd6_1 = new HtmlGenericControl("div");
            panel_colmd6_1.Attributes.Add("class", "col-md-6");
            panel_colmd6_1.Controls.Add(left);
            HtmlGenericControl panel_colmd6_2 = new HtmlGenericControl("div");
            panel_colmd6_2.Attributes.Add("class", "col-md-6");
            panel_colmd6_2.Controls.Add(right);
            HtmlGenericControl panel_row = new HtmlGenericControl("div");
            panel_row.Attributes.Add("class", "row");
            panel_row.Controls.Add(panel_colmd6_1);
            panel_row.Controls.Add(panel_colmd6_2);

            //panel_ajust.Controls.Add(left);
            panel_ajust.Controls.Add(panel_row);

            panel_heading.Controls.Add(panel_ajust);

            panel_warning.Controls.Add(panel_heading);
            panel_warning.Controls.Add(panel_body);

            panel.Controls.Add(panel_warning);
            row_div.Controls.Add(panel);
            fatherPanel.Controls.Add(row_div);
            pai = fatherPanel;
        }

        public void CreateTable(ref UpdatePanel pai)
        {
            HtmlGenericControl row_div = new HtmlGenericControl("div");
            row_div.Attributes.Add("class", "row");

            HtmlGenericControl br = new HtmlGenericControl("div");
            br.Attributes.Add("class", "col-md-12");
            br.InnerHtml = "<br />";

            row_div.Controls.Add(br);

            //<input class="btn btn-default" type="button" value="Input">

            HtmlGenericControl panel = new HtmlGenericControl("div");
            panel.Attributes.Add("class", "col-md-12");

            HtmlGenericControl panel_warning = new HtmlGenericControl("div");
            panel_warning.Attributes.Add("class", "panel-hcc panel-hcc-default");

            HtmlGenericControl panel_heading = new HtmlGenericControl("div");
            panel_heading.Attributes.Add("class", "panel-hcc-heading");

            HtmlGenericControl panel_ajust = new HtmlGenericControl("div");
            panel_ajust.Attributes.Add("class", "panel-hcc-ajust");

            HtmlGenericControl left = new HtmlGenericControl("div");
            left.Attributes.Add("class", "left");
            if (!string.IsNullOrEmpty(LeftTitle) && string.IsNullOrEmpty(LeftValue))
                left.InnerHtml = "<b>" + LeftTitle + "</b>";
            else if (!string.IsNullOrEmpty(LeftTitle) && !string.IsNullOrEmpty(LeftValue))
                left.InnerHtml = "<b>" + LeftTitle + ": </b>" + LeftValue;

            HtmlGenericControl right = new HtmlGenericControl("div");
            right.Attributes.Add("class", "right");
            if (!string.IsNullOrEmpty(RightTitle) && string.IsNullOrEmpty(RightValue))
                right.InnerHtml = "<b>" + RightTitle + "</b>";
            else if (!string.IsNullOrEmpty(RightTitle) && !string.IsNullOrEmpty(RightValue))
                right.InnerHtml = "<b>" + RightTitle + ": </b>" + RightValue;

            HtmlGenericControl panel_body = new HtmlGenericControl("div");
            panel_body.Attributes.Add("class", "panel-hcc-body");

            HtmlGenericControl table = new HtmlGenericControl("table");
            table.Attributes.Add("class", "table-hcc table-hcc-striped table-hcc-bordered table-hcc-hover dataTable no-footer dtr-inline");
            table.Attributes.Add("role", "grid");
            table.Attributes.Add("aria-describedby", "dataTables");
            table.Style.Add(HtmlTextWriterStyle.Width, "100%");

            HtmlGenericControl role_row = new HtmlGenericControl("tr");
            role_row.Attributes.Add("role", "row");

            HtmlGenericControl sorting;
            foreach (TableColunas column in columns)
            {
                sorting = new HtmlGenericControl("th");
                sorting.Attributes.Add("tabindex", column.tabindex.ToString());
                sorting.Attributes.Add("aria-controls", column.aria_controls);
                sorting.Attributes.Add("rowspan", column.rowspan.ToString());
                sorting.Attributes.Add("colspan", column.colspan.ToString());
                sorting.Attributes.Add("aria-sort", column.aria_sort);
                sorting.Attributes.Add("aria-label", column.aria_label);
                sorting.InnerText = column.Text;
                sorting.Style.Value = column.Style.Value;
                role_row.Controls.Add(sorting);
            }

            HtmlGenericControl thead = new HtmlGenericControl("thead");
            thead.Controls.Add(role_row);

            HtmlGenericControl tbody = new HtmlGenericControl("tbody");

            HtmlGenericControl tr_odd = new HtmlGenericControl("tr");
            HtmlGenericControl tr_even;

            HtmlGenericControl td = new HtmlGenericControl("td");
            if (rows.Count > 0)
            {
                bool even = false;
                foreach (TableLinhas row in rows)
                {
                    if (!even)
                    {
                        tr_odd = new HtmlGenericControl("tr");
                        tr_odd.Attributes.Add("class", "gradeA odd");
                        tr_odd.Attributes.Add("role", "row");

                        foreach (TableCelulas cell in row.cells)
                        {
                            td = new HtmlGenericControl("td");
                            td.Attributes.Add("class", "sorting_1");
                            td.InnerText = cell.Value;
                            td.Style.Value = cell.Style.Value;
                            tr_odd.Controls.Add(td);
                        }

                        tbody.Controls.Add(tr_odd);
                        even = true;
                    }
                    else
                    {
                        tr_even = new HtmlGenericControl("tr");
                        tr_even.Attributes.Add("class", "gradeA even");
                        tr_even.Attributes.Add("role", "row");

                        foreach (TableCelulas cell in row.cells)
                        {
                            td = new HtmlGenericControl("td");
                            td.Attributes.Add("class", "sorting_1");
                            td.InnerText = cell.Value;
                            td.Style.Value = cell.Style.Value;
                            tr_even.Controls.Add(td);
                        }

                        tbody.Controls.Add(tr_even);
                        even = false;
                    }
                }
            }
            else
            {
                td = new HtmlGenericControl("td");
                td.Attributes.Add("class", "sorting_1");
                td.Style.Add("text-align", "center");
                td.InnerText = Empty;
                td.Attributes.Add("colspan", "7");
                tr_odd.Controls.Add(td);

                tbody.Controls.Add(tr_odd);
            }

            table.Controls.Add(thead);
            table.Controls.Add(tbody);

            panel_body.Controls.Add(table);

            HtmlGenericControl panel_colmd6_1 = new HtmlGenericControl("div");
            panel_colmd6_1.Attributes.Add("class", "col-md-6");
            panel_colmd6_1.Controls.Add(left);
            HtmlGenericControl panel_colmd6_2 = new HtmlGenericControl("div");
            panel_colmd6_2.Attributes.Add("class", "col-md-6");
            panel_colmd6_2.Controls.Add(right);
            HtmlGenericControl panel_row = new HtmlGenericControl("div");
            panel_row.Attributes.Add("class", "row");
            panel_row.Controls.Add(panel_colmd6_1);
            panel_row.Controls.Add(panel_colmd6_2);

            //panel_ajust.Controls.Add(left);
            panel_ajust.Controls.Add(panel_row);

            panel_heading.Controls.Add(panel_ajust);

            panel_warning.Controls.Add(panel_heading);
            panel_warning.Controls.Add(panel_body);

            panel.Controls.Add(panel_warning);
            row_div.Controls.Add(panel);
            fatherUpdate.ContentTemplateContainer.Controls.Add(row_div);
            pai = fatherUpdate;
        }
    }
}