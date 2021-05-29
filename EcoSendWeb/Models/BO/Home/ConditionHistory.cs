using EcoSendWeb.Extensions;
using System;
using static EcoSendWeb.Infrastructure.Defs;

namespace EcoSendWeb.Models.BO.Home
{
    public class ConditionHistory
    {
        public Guid Id { get; set; }
        public Guid AirInfoId { get; set; }
        public ConditionType ConditionType { get; set; }
        public DateTime? Updated { get; set; }
        public string Comment { get; set; }

        [ExcelColNameAttribute(label: "Min Price", colNumber: 7, convertType: ConvertType.Normal, colFormat: "$#,##0.00")]
        public double? MinPrice { get; set; }

        [ExcelColNameAttribute(label: "Mkt Price", colNumber: 8, convertType: ConvertType.Normal, colFormat: "$#,##0.00")]
        public double? MaxPrice { get; set; }

        public object[] GetObjectProperties()
        {
            return new object[]
            {
                this.MinPrice,
                this.MaxPrice
            };
        }
    }
}