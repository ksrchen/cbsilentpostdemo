using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using mpps.Controllers;
using mpps.domain;


namespace mpps
{
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        IProfileDomain _profileDomain = new ProfileDomain();
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                var controller = actionContext.ControllerContext.Controller as MppsBaseController;
                controller.Profile = _profileDomain.Get(1);

                var timestampHeader = actionContext.Request.Headers.FirstOrDefault(p => p.Key == "x-timestamp");
                var authorizationHeader = actionContext.Request.Headers.FirstOrDefault(p => p.Key == "Authorization");

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