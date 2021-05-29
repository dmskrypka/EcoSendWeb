using System;
using System.Globalization;
using static EcoSendWeb.Infrastructure.Defs;

namespace EcoSendWeb.Models.View.Home
{
    public class ConditionVM
    {
        private readonly CultureInfo ci = CultureInfo.CurrentCulture;
        private readonly string currencyFormat = "C";

        public Guid Id { get; set; }

        public ConditionType ConditionType { get; set; }
        public DateTime? Updated { get; set; }
        public string Comment { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public string StrUpdated
        {
            get => this.Updated?.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string StrChartUpdated
        {
            get => this.Updated?.ToString("yyyy-MM-dd");
        }

        public string StrMinPrice
        {
            get => this.MinPrice?.ToString(currencyFormat, ci);
        }

        public string StrMaxPrice
        {
            get => this.MaxPrice?.ToString(currencyFormat, ci);
        }
    }
}