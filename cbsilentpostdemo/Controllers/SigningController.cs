using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace cbsilentpostdemo.Controllers
{
    public class SigningController : ApiController
    {
        public HttpResponseMessage Post([FromBody]FormDataCollection values)
        {
            var dictionary = values.ToDictionary(p => p.Key, q => q.Value);
            string signature = security.sign(dictionary);
            return Request.CreateResponse(HttpStatusCode.OK, new {@signature=signature});
        }
    }
}