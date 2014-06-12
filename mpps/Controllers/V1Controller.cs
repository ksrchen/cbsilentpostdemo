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
            List<string> contents = new List<string>();
            var path = HostingEnvironment.MapPath("~/scripts/hmac-sha256.js");
            contents.Add(File.ReadAllText(path));

            path = HostingEnvironment.MapPath("~/scripts/enc-base64-min.js");
            contents.Add(File.ReadAllText(path));

#if DEBUG
            path = HostingEnvironment.MapPath("~/scripts/mpps.min.js");
#else
            path = HostingEnvironment.MapPath("~/scripts/mpps.min.js");
#endif
            contents.Add(File.ReadAllText(path));

            var link = Url.Link("DefaultApi", new { controller = "Signing" });
            var uri = new Uri(link);
            var key = KeyStoreManager.createNewKey();

            contents.Add(String.Format("mpps.init('{0}','{1}','{2}');", 
                uri.GetLeftPart(UriPartial.Authority),
                pts.domain.SecurityDomain.Encrypt(key.Id.ToString()), 
                key.Value));

            var rsp =  new HttpResponseMessage
            {
                Content = new StringContent(
                    string.Join(" ", contents),
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
