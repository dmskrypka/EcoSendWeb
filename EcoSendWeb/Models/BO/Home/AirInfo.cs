using EcoSendWeb.Extensions;
using System;
using System.Collections.Generic;

namespace EcoSendWeb.Models.BO.Home
{
    [Serializable]
    public class AirInfo
    {
        public Guid Id { get; set; }

        [ExcelColNameAttribute(label: "Segment", colNumber: 1)]
        public string Segment { get; set; }

        [ExcelColNameAttribute(label: "Part Number", colNumber: 2)]
        public string PartNumber { get; set; }

        [ExcelColNameAttribute(label: "Description", colNumber: 3, convertType: ConvertType.None, notAccessibleValue: "###Not on File")]
        public string Description { get; set; }

        [ExcelColNameAttribute(label: "ATA", colNumber: 4, convertType: ConvertType.Normal)]
        public int ATA { get; set; }

        [ExcelColNameAttribute(label: "ABC", colNumber: 5, convertType: ConvertType.None, notAccessibleValue: "No Data")]
        public string ABC { get; set; }

        [ExcelColNameAttribute(label: "Mfg Name", colNumber: 6)]
        public string MfgName { get; set; }

        [ExcelColNameAttribute(label: "12 Months Total Sales", colNumber: 13, convertType: ConvertType.Normal)]
        public int TwlvMonthsTotalSales { get; set; }

        [ExcelColNameAttribute(label: "12 Months Total Quotes", colNumber: 14, convertType: ConvertType.Normal)]
        public int TwlvMonthsTotalQuotes { get; set; }

        [ExcelColNameAttribute(label: "12 Months Total No Bids", colNumber: 15, convertType: ConvertType.Normal)]
        public int TwlvMonthsTotalNoBids { get; set; }

        [ExcelColNameAttribute(label: "Repair Price", colNumber: 16, convertType: ConvertType.Normal, colFormat: "$#,##0.00")]
        public double ReparePrice { get; set; }

        [ExcelColNameAttribute(label: "Suggested Purchase Price", colNumber: 17, convertType: ConvertType.Normal, colFormat: "$#,##0.00")]
        public double SuggestedPurchasePrice { get; set; }

        [ExcelColNameAttribute(label: "Mod", colNumber: 18, convertType: ConvertType.YesNoToBool)]
        public bool Mod { get; set; }

        [ExcelColNameAttribute(label: "Aircraft", colNumber: 19)]
        public string Aircraft { get; set; }

        public IEnumerable<CategoryInfo> Categories { get; set; }

        public object[] GetObjectProperties()
        {
            return new object[]
            {
                this.Segment,
                this.PartNumber,
                this.Description,
                this.ATA,
                this.ABC,
                this.MfgName,
                this.TwlvMonthsTotalSales,
                this.TwlvMonthsTotalQuotes,
                this.TwlvMonthsTotalNoBids,
                this.ReparePrice,
                this.SuggestedPurchasePrice,
                this.Mod ? "Yes" : "No",
                this.Aircraft
            };
        }
    }
}