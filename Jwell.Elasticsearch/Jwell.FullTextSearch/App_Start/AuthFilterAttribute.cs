using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Jwell.Sample.App_Start
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //如果用户方位的Action带有AllowAnonymousAttribute，则不进行授权验证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            var loginUser = HttpContext.Current.Session["loginUser"];
            if (loginUser == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                };
            }
        }
    }
}