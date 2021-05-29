using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EcoSendWeb.Infrastructure.Defs;

namespace EcoSendWeb.Models.View.Home
{
    public class CategoryVM
    {
        public Guid Id { get; set; }

        public Guid AirInfoId { get; set; }

        public CategoryType CategoryType { get; set; }

        [Display(Name = "COMPANY")]
        public string Company { get; set; }

        [Display(Name = "CONDITION")]
        public ConditionType ConditionType { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "QTY")]
        public int Qty { get; set; }

        [Display(Name = "LT DAYS")]
        public int LtDays { get; set; }

        [Display(Name = "UNIT PRICE")]
        public double UnitPrice { get; set; }

        [Display(Name = "SERIAL NUMBER")]
        public string SerialNumber { get; set; }

        [Display(Name = "COMMENT")]
        public string Comment { get; set; }

        [Display(Name = "DATE")]
        public string StrDate
        {
            get => this.Date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public bool Valid()
        {
            if (String.IsNullOrEmpty(this.Company)) return false;
            if (this.Date == default(DateTime)) return false;
            if (this.Qty == 0) return false;

            return true;
        }

    }
}