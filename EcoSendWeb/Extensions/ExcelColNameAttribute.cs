using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoSendWeb.Extensions
{
    public enum ConvertType
    {
        None,
        Normal,
        YesNoToBool
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColNameAttribute : Attribute
    {
        public string Label { get; }
        public int ColNumber { get; }
        public ConvertType ConvertType { get; set; }
        public string NotAccessibleValue { get; set; }
        public string ColFormat { get; set; }
        public ExcelColNameAttribute(string label, int colNumber, ConvertType convertType = ConvertType.None, string notAccessibleValue = null, string colFormat = null)
        {
            this.Label = label;
            this.ColNumber = colNumber;
            this.ConvertType = convertType;
            this.NotAccessibleValue = notAccessibleValue;
            this.ColFormat = colFormat;
        }

    }
}