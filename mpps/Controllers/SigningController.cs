using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using mpps.domain;

namespace mpps.Controllers
{
    public class SigningController : MppsBaseController
    {
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [AuthorizationFilter]
        public HttpResponseMessage Post([FromBody]IDictionary<string, string> values)
        {
            try
            {
                values["access_key"] = Profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "access_key").SettingValue;
                values["profile_id"] = Profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "profile_id").SettingValue;
                values["url"] = Profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "tokencreateurl").SettingValue;

                var secret = Profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "secret").SettingValue;

                values["signature"] = SecurityDomain.sign(values, secret);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            catch (Exception exp)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exp);
            }
        }        
    }
}