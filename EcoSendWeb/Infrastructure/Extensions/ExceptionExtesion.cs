using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoSendWeb.Infrastructure.Extensions
{
    public static class ExceptionExtesion
    {
        public static string FullErrorMessage(this Exception self)
        { 
            return GenerateMessage(self, "");
        }

        private static string GenerateMessage(Exception ex, string msg)
        {
            if(ex != null)
            {
                return String.Concat(msg, "\n", GenerateMessage(ex.InnerException, msg));
            }
            else
            {
                return msg;
            }
        }
    }
}