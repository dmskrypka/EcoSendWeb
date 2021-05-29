using EcoSendWeb.Extensions;
using EcoSendWeb.Models.BO.Home;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using static EcoSendWeb.Infrastructure.Defs;
using XslExcel = Microsoft.Office.Interop.Excel;

namespace EcoSendWeb.Helpers
{
    public class ExcelHelper
    {
        public static IDictionary<AirInfo, List<ConditionHistory>> ReadExcel(HttpPostedFileBase file)
        {
            DateTime now = DateTime.Now;
            Dictionary<AirInfo, List<ConditionHistory>> result = new Dictionary<AirInfo, List<ConditionHistory>>();
            PropertyInfo[] aiProps = typeof(AirInfo).GetProperties().Where(x => x.CustomAttributes.Any()).ToArray();
            PropertyInfo[] chProps = typeof(ConditionHistory).GetProperties().Where(x => x.CustomAttributes.Any()).ToArray();

            string fileName = file.FileName;
            string fileContentType = file.ContentType;

            if (fileContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                throw new Exception(@"Please convert your\'s file to xlsx format.");
            }

            using (ExcelPackage package = new ExcelPackage(file.InputStream))
            {
                ExcelWorksheets currentSheet = package.Workbook.Worksheets;
                if (currentSheet.Count == 0)
                {
                    throw new Exception("A workbook must contain at least one worksheet.");
                }

                ExcelWorksheet workSheet = currentSheet.First();
                int noOfCol = workSheet.Dimension.End.Column;
                int noOfRow = workSheet.Dimension.End.Row;

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    bool emptyAiObj = true;
                    bool ignoreAiRow = false;
                    AirInfo airInfo = new AirInfo();
                    List<ConditionHistory> conditions = new List<ConditionHistory>();

                    foreach (PropertyInfo propertyInfo in aiProps)
                    {
                        int colNum = (int)propertyInfo.CustomAttributes.First().ConstructorArguments[1].Value;
                        ConvertType converType = (ConvertType)propertyInfo.CustomAttributes.First().ConstructorArguments[2].Value;
                        string notAccessibleValue = (string)propertyInfo.CustomAttributes.First().ConstructorArguments[3].Value;

                        if (workSheet.Cells[rowIterator, colNum] != null && workSheet.Cells[rowIterator, colNum].Value != null)
                        {
                            string strValue = workSheet.Cells[rowIterator, colNum].Value.ToString().Trim();

                            if (!ignoreAiRow)
                                ignoreAiRow = strValue == notAccessibleValue;
                            else break;

                            if (converType == ConvertType.Normal)
                            {
                                Type propType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                                Type[] argTypes = { typeof(string), propType.MakeByRefType() };
                                var tryParseMethodInfo = propType.GetMethod("TryParse", argTypes);
                                if (tryParseMethodInfo != null)
                                {
                                    object[] args = { strValue, null };
                                    var successfulParse = (bool)tryParseMethodInfo.Invoke(null, args);
                                    if (successfulParse)
                                    {
                                        propertyInfo.SetValue(airInfo, args.Last());
                                        emptyAiObj = false;
                                    }
                                }
                            }
                            else if (converType == ConvertType.YesNoToBool && propertyInfo.PropertyType == typeof(bool))
                            {
                                if (new string[] { "yes", "no" }.Contains(value: strValue.ToLower()))
                                {
                                    propertyInfo.SetValue(airInfo, value: strValue.ToLower() == "yes");
                                    emptyAiObj = false;
                                }
                            }
                            else
                            {
                                propertyInfo.SetValue(airInfo, strValue);
                                emptyAiObj = false;
                            }
                        }
                    }

                    if (!ignoreAiRow)
                    {
                        airInfo.Id = Guid.NewGuid();

                        int step = chProps.Count();
                        string[] cTypes = Enum.GetNames(typeof(ConditionType));
                        for (int i = 0; i < cTypes.Count(); i++)
                        {
                            bool emptyChObj = true;
                            bool ignoreChRow = false;
                            ConditionHistory ch = new ConditionHistory();

                            foreach (PropertyInfo propertyInfo in chProps)
                            {
                                int colNum = ((int)propertyInfo.CustomAttributes.First().ConstructorArguments[1].Value) + (step * i);
                                ConvertType converType = (ConvertType)propertyInfo.CustomAttributes.First().ConstructorArguments[2].Value;
                                string notAccessibleValue = (string)propertyInfo.CustomAttributes.First().ConstructorArguments[3].Value;

                                if (workSheet.Cells[rowIterator, colNum] != null && workSheet.Cells[rowIterator, colNum].Value != null)
                                {
                                    string strValue = workSheet.Cells[rowIterator, colNum].Value.ToString().Trim();

                                    if (!ignoreChRow)
                                        ignoreChRow = strValue == notAccessibleValue;
                                    else break;

                                    if (converType == ConvertType.Normal)
                                    {
                                        Type propType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                                        Type[] argTypes = { typeof(string), propType.MakeByRefType() };
                                        var tryParseMethodInfo = propType.GetMethod("TryParse", argTypes);
                                        if (tryParseMethodInfo != null)
                                        {
                                            object[] args = { strValue, null };
                                            var successfulParse = (bool)tryParseMethodInfo.Invoke(null, args);
                                            if (successfulParse)
                                            {
                                                propertyInfo.SetValue(ch, args.Last());
                                                emptyChObj = false;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!ignoreChRow && !emptyChObj)
                            {
                                ch.AirInfoId = airInfo.Id;
                                ch.ConditionType = (ConditionType)(i + 1);
                                ch.Updated = now;
                                ch.Comment = "created from " + fileName;

                                conditions.Add(ch);
                            }
                        }
                    }

                    if (!ignoreAiRow && !emptyAiObj)
                    {
                        result.Add(airInfo, conditions);
                    }
                }
            }

            return result;
        }

        public static byte[] SaveExceFile(IDictionary<AirInfo, IEnumerable<ConditionHistory>> content)
        {
            Dictionary<int, string> headers = new Dictionary<int, string>();
            Dictionary<int, string> colFormats = new Dictionary<int, string>();

            PropertyInfo[] aiProps = typeof(AirInfo).GetProperties().Where(x => x.CustomAttributes.Any()).ToArray();
            foreach (PropertyInfo propertyInfo in aiProps)
            {
                string colName = (string)propertyInfo.CustomAttributes.First().ConstructorArguments[0].Value;
                int colNum = (int)propertyInfo.CustomAttributes.First().ConstructorArguments[1].Value;
                string colFormat = (string)propertyInfo.CustomAttributes.First().ConstructorArguments[4].Value;

                if (!String.IsNullOrEmpty(colFormat))
                {
                    colFormats.Add(colNum, colFormat);
                }

                headers.Add(colNum, colName);
            }

            int conditionsColStart = -1;
            PropertyInfo[] chProps = typeof(ConditionHistory).GetProperties().Where(x => x.CustomAttributes.Any()).ToArray();
            string[] cTypes = Enum.GetNames(typeof(ConditionType));
            int step = chProps.Count();
            for (int i = 0; i < cTypes.Count(); i++)
            {
                foreach (PropertyInfo propertyInfo in chProps)
                {
                    string colName = String.Concat(cTypes[i], " ", (string)propertyInfo.CustomAttributes.First().ConstructorArguments[0].Value);
                    int colNum = ((int)propertyInfo.CustomAttributes.First().ConstructorArguments[1].Value) + (step * i);
                    string colFormat = (string)propertyInfo.CustomAttributes.First().ConstructorArguments[4].Value;

                    if (!String.IsNullOrEmpty(colFormat))
                    {
                        colFormats.Add(colNum, colFormat);
                    }

                    if (conditionsColStart < 0)
                        conditionsColStart = colNum - 1;

                    headers.Add(colNum, colName);
                }
            }

            List<object[]> rows = new List<object[]> { headers.OrderBy(x => x.Key).Select(y => y.Value).ToArray() };

            foreach (KeyValuePair<AirInfo, IEnumerable<ConditionHistory>> kvp in content)
            {
                List<object> list = new List<object>(kvp.Key.GetObjectProperties());
                for (int i = 0; i < cTypes.Count(); i++)
                {
                    int index = conditionsColStart + (step * i);
                    list.InsertRange(index, kvp.Value.ElementAt(i).GetObjectProperties());
                }

                rows.Add(list.ToArray());
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                package.Workbook.Worksheets.Add("Worksheet1");
                string headerRange = String.Concat("A1:", Char.ConvertFromUtf32(rows[0].Length + 64), rows.Count());
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Worksheet1"];
                worksheet.Cells[headerRange].LoadFromArrays(rows);

                int rowCount = rows.Count;
                foreach (KeyValuePair<int, string> kvp in colFormats)
                {
                    worksheet.Cells[2, kvp.Key, rowCount, kvp.Key].Style.Numberformat.Format = kvp.Value;
                }

                return package.GetAsByteArray();
            }
        }
    }
}