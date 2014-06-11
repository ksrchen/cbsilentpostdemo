using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using pts.Controllers;
using pts.domain;


namespace pts
{
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        IProfileDomain _profileDomain = new ProfileDomain();
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                var authorizationHeader = actionContext.Request.Headers.FirstOrDefault(p => p.Key == "Authorization");
                if (!actionContext.Request.Headers.Contains("Authorization"))
                {
                    throw new Exception("Authorization header is required");
                }
                int profileId = 0;
                if (authorizationHeader.Value.First().Length > 0)
                {
                    var parts = authorizationHeader.Value.First().Split(new char[] { ':' });
                    string profile = SecurityDomain.Decrypt(parts[0]);
                    int.TryParse(profile, out profileId);

                    if (parts.Count() < 3)
                    {
                        throw new Exception("Invalid authorization header");
                    }

                    var keyId = pts.domain.SecurityDomain.Decrypt(parts[1]);
                    var key = KeyStoreManager.getKey(keyId);

                    if (key == null)
                    {
                        throw new Exception(string.Format("key not found {0}", keyId));
                    }

                    var time = key.Date - DateTime.Now;
                    if (Math.Abs(time.TotalMinutes) > 15)
                    {
                        throw new Exception(string.Format("key expired {0}", keyId));
                    }

                    validateSignature(parts[0], parts[1], key.Value, parts[2]);
                }

                var controller = actionContext.ControllerContext.Controller as MppsBaseController;
                controller.Profile = _profileDomain.Get(profileId);

                if (controller.Profile == null)
                {
                    throw new Exception(string.Format("Profile {0} not found ", profileId));
                }

                return continuation();
            }
            catch (Exception exp)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Forbidden, exp);
                var source = new TaskCompletionSource<HttpResponseMessage>();
                source.SetResult(actionContext.Response);
                return source.Task;
            }
        }

        private void validateSignature(string profile, string key, string secret, string signature)
        {
            var text = string.Format("profile={0},key={1}", profile, key);
            var hash = pts.domain.SecurityDomain.sign(text, secret);
            if (hash != signature)
            {
                throw new Exception("signature mismatch");
            }
        }
    }
}