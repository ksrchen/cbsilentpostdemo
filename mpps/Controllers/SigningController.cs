using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using pts.domain;

namespace pts.Controllers
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
                var rootUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                values["access_key"] = Profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "access_key").SettingValue;
                values["profile_id"] = Profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "profile_id").SettingValue;
                values["url"] = Profile.Provider.ProviderSettings.FirstOrDefault(p => p.SettingName == "tokencreateurl").SettingValue;
                values["override_custom_receipt_page"] = rootUrl + "/receipt.aspx?profile=" + Profile.ProfileID.ToString();
                values["signed_date_time"] = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
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