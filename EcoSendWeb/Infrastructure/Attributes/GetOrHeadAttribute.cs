using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoSendWeb.Infrastructure.Attributes
{
    public class HttpGetOrHeadAttribute : ActionMethodSelectorAttribute
    {
        private static AcceptVerbsAttribute attr = new AcceptVerbsAttribute(HttpVerbs.Get | HttpVerbs.Head);

        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            return attr.IsValidForRequest(controllerContext, methodInfo);
        }
    }
}