using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using static EcoSendWeb.Infrastructure.Defs;

namespace EcoSendWeb.Models.View.Home
{
    public class AirInfoVM
    {
        private readonly CultureInfo ci = CultureInfo.CurrentCulture;
        private readonly string currencyFormat = "C";

        public Guid Id { get; set; }

        [Display(Name = "SEGMENT")]
        public string Segment { get; set; }

        [Display(Name = "PART NUMBER")]
        public string PartNumber { get; set; }

        [Display(Name = "DESCRIPTION")]
        public string Description { get; set; }

        [Display(Name = "ATA")]
        public int ATA { get; set; }

        [Display(Name = "ABC")]
        public string ABC { get; set; }

        [Display(Name = "MANUFACTURER")]
        public string MfgName { get; set; }

        public double ReparePrice { get; set; }
        public double SuggestedPurchasePrice { get; set; }

        [Display(Name = "TOTAL SALES")]
        public int TwlvMonthsTotalSales { get; set; }

        [Display(Name = "TOTAL QUOTES")]
        public int TwlvMonthsTotalQuotes { get; set; }

        [Display(Name = "TOTAL NO BIDS")]
        public int TwlvMonthsTotalNoBids { get; set; }

        [Display(Name = "MOD")]
        public bool Mod { get; set; }

        [Display(Name = "AIRCRAFT")]
        public string Aircraft { get; set; }

        public SelectList ModsList
        {
            get
            {
                return new SelectList(new List<bool>
                {
                    true,
                    false
                });
            }
        }

        [Display(Name = "REPAIR PRICE")]
        public string StrReparePrice
        {
            get => this.ReparePrice.ToString(currencyFormat, ci);
        }

        [Display(Name = "AV. PURCHASE PRICE")]
        public string StrSuggestedPurchasePrice
        {
            get => this.SuggestedPurchasePrice.ToString(currencyFormat, ci);
        }

        public IEnumerable<ConditionVM> Conditions { get; set; }

        public IEnumerable<string> CategoryNames
        {
            get
            {
                string[] categories = Enum.GetNames(typeof(CategoryType));
                for (int i = 0; i < categories.Length; i++)
                {
                    categories[i] = categories[i].ToUpper().Replace("_", " ");
                }

                return categories;
            }
        }

        public IEnumerable<CategoryVM> Categories { get; set; }


        public bool Validate()
        {
            if (String.IsNullOrEmpty(this.Segment)) return false;
            if (String.IsNullOrEmpty(this.PartNumber)) return false;
            if (String.IsNullOrEmpty(this.ABC)) return false;

            return true;
        }
    }
}