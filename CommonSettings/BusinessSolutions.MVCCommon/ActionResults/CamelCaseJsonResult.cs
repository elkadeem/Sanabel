using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessSolutions.MVCCommon
{
    public class CamelCaseJsonResult : JsonResult
    {
        public CamelCaseJsonResult(object data, JsonRequestBehavior jsonRequestBehavior)
        {
            this.Data = data;
            this.JsonRequestBehavior = jsonRequestBehavior;

        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("This request is blocked");

            if (this.Data == null)
                return;

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                MaxDepth = this.RecursionLimit ?? 3,

            };

            response.Write(JsonConvert.SerializeObject(this.Data, settings));
        }
    }
}
