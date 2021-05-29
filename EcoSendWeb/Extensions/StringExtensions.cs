using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace EcoSendWeb.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveDiacritics(this string strText)
        {
            if (String.IsNullOrWhiteSpace(strText))
            {
                return String.Empty;
            }

            var chars =
                from c in strText.Normalize(NormalizationForm.FormD).ToCharArray()
                let uc = CharUnicodeInfo.GetUnicodeCategory(c)
                where uc != UnicodeCategory.NonSpacingMark
                select c;

            return new string(chars.ToArray()).Normalize(NormalizationForm.FormC);
        }
    }
}