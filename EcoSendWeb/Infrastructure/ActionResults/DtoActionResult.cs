using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Web.Mvc;

namespace EcoSendWeb.Infrastructure.ActionResults
{
    public class DtoActionResult : ActionResult
    {
        internal readonly static JsonSerializerSettings settings;

        private object dtoData;

        static DtoActionResult()
        {
            settings = new JsonSerializerSettings()
            {
                Culture = CultureInfo.InvariantCulture,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public DtoActionResult(object data)
        {
            this.dtoData = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.RequestContext.HttpContext.Response.ContentType = "application/json";
            context.RequestContext.HttpContext.Response.Write(JsonConvert.SerializeObject(this.dtoData, settings));
        }
    }
}