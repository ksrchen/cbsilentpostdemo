using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace mpps.Controllers
{
    public class SigningController : ApiController
    {
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
        public HttpResponseMessage Post([FromBody]FormDataCollection values)
        {
            try
            {
                var dictionary = values.ToDictionary(p => p.Key, q => q.Value);
                string signature = security.sign(dictionary);
                return Request.CreateResponse(HttpStatusCode.OK, new { @signature = signature });
            }
            catch (Exception exp)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exp);
            }
        }
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {@greet="hello" });
        }
    }
}