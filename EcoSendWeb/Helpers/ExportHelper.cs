//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Linq;
//using System.Web;
//using Pythagoras.Export.Xlsx;

//namespace EcoSendWeb.Helpers
//{
//    public static class ExportHelper
//    {

//        public static DataTable ToDataTable(this IEnumerable items) {
//            DataTable t = new DataTable();
//            var itemType = items.GetType().GetGenericArguments()[0];
//            var props = itemType.GetProperties();

//            foreach (var p in props) {
//                Type pt = p.PropertyType;

//                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>)) {
//                    pt = pt.GetGenericArguments()[0];
//                }

//                var displayName = p.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>().FirstOrDefault();

//                t.Columns.Add(displayName == null ? p.Name : displayName.DisplayName, pt);
//            }

//            t.BeginLoadData();

//            foreach (var i in items) {
//                DataRow r = t.NewRow();

//                foreach (var p in props) {
//                    var v = p.GetValue(i);

//                    var displayName = p.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>().FirstOrDefault();

//                    r[displayName == null ? p.Name : displayName.DisplayName] = v ?? DBNull.Value;
//                }

//                t.Rows.Add(r);
//            }

//            t.EndLoadData();

//            return t;
//        }

//        public static void ExportToExcel(string fileName, string sheetName, DataTable data, HttpContextBase ctx) {
//            HttpResponseBase resp = ctx.Response;

//            if (string.IsNullOrEmpty(fileName)) {
//                fileName = "export";
//            }

//            if (string.IsNullOrEmpty(sheetName)) {
//                sheetName = "Data";
//            }

//            if (sheetName.Length > 30) {
//                sheetName = sheetName.Substring(0, 30);
//            }

//            resp.Clear();
//            resp.Cache.SetCacheability(HttpCacheability.NoCache);
//            resp.BufferOutput = false;
//            resp.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//            resp.AppendHeader("Content-Disposition", String.Format("attachment; filename=\"{0}\"", Uri.EscapeDataString(fileName + ".xlsx")));

//            using (XlsxExporter export = new XlsxExporter()) {
//                export.AddSheet(data.DefaultView, sheetName);
//                export.Save(resp.OutputStream);
//            }

//            resp.Flush();
//            resp.End();
//        }

//        public static void ExportToExcel(string fileName, DataSet data, HttpContextBase ctx) {
//            HttpResponseBase resp = ctx.Response;

//            if (string.IsNullOrEmpty(fileName)) {
//                fileName = "export";
//            }

//            resp.Clear();
//            resp.Cache.SetCacheability(HttpCacheability.NoCache);
//            resp.BufferOutput = false;
//            resp.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//            resp.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", Uri.EscapeDataString(fileName + " " + DateTime.Now.ToString().GetHashCode().ToString("x") + ".xlsx")));

//            using (XlsxExporter export = new XlsxExporter()) {
//                int sheetPos = 0;
//                foreach (DataTable t in data.Tables) {
//                    string sheetName = string.IsNullOrEmpty(t.TableName) ? "data" + sheetPos : t.TableName;

//                    if (sheetName.Length > 30) {
//                        sheetName = sheetName.Substring(0, 30);
//                    }

//                    export.AddSheet(t.DefaultView, sheetName);
//                }

//                ++sheetPos;

//                export.Save(resp.OutputStream);
//            }

//            resp.Flush();
//            resp.End();
//        }
//    }
//}