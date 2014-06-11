using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace pts.Controllers
{
    public class V1Controller : ApiController
    {
        public HttpResponseMessage Get()
        {
            var path = HostingEnvironment.MapPath("~/scripts/hmac-sha256.js");
            var content1 = File.ReadAllText(path);

            path = HostingEnvironment.MapPath("~/scripts/enc-base64-min.js");
            var content2 = File.ReadAllText(path);

#if DEBUG
            path = HostingEnvironment.MapPath("~/scripts/mpps.js");
#else
            path = HostingEnvironment.MapPath("~/scripts/mpps.min.js");
#endif
            var content3 = File.ReadAllText(path);

            var link = Url.Link("DefaultApi", new { controller = "Signing" });
            var uri = new Uri(link);
            var content4 = String.Format("mpps.rootUrl='{0}';", uri.GetLeftPart(UriPartial.Authority));

            var rsp =  new HttpResponseMessage
            {
                Content = new StringContent(
                    content1 + content2 + content3 + content4,
                    Encoding.UTF8,
                    "application/javascript"),                
            };
            rsp.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoStore = true,
                NoCache = true,
                MustRevalidate = true,
                Private= true,
            };

            return rsp;
        }

    }
}
