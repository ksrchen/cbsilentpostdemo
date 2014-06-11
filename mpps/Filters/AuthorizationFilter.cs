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

                    if (parts.Count() < 2)
                    {
                        throw new Exception("Invalid authorization header");
                    }

                    var signature = parts[1];

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
    }
}