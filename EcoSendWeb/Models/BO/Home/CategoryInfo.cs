using System;
using static EcoSendWeb.Infrastructure.Defs;

namespace EcoSendWeb.Models.BO.Home
{
    public class CategoryInfo
    {
        public Guid Id { get; set; }
        public Guid AirInfoId { get; set; }
        public CategoryType CategoryType { get; set; }
        public string Company { get; set; }
        public ConditionType ConditionType { get; set; }
        public DateTime Date { get; set; }
        public int Qty { get; set; }
        public int LtDays { get; set; }
        public double UnitPrice { get; set; }
        public string SerialNumber { get; set; }
        public string Comment { get; set; }
    }
}