using Jwell.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace Jwell.FullTextSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 应用程序启动
        /// </summary>
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new JwellBootstrap<JwellWebModule>().Start();
        }

        public override void Init()
        {
            this.AuthenticateRequest += WebApiApplication_AuthenticateRequest;
            base.Init();
        }

        private void WebApiApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }

    public class JwellSessionManager : ISessionIDManager
    {
        private const string sessionKey = "auth";
        public string CreateSessionID(HttpContext context)
        {
            return Guid.NewGuid().ToString("N");
        }

        public string GetSessionID(HttpContext context)
        {
            string id = context.Request.Headers[sessionKey];
            if (!Validate(id)) id = null;
            return id;
        }

        public void Initialize()
        {

        }

        public bool InitializeRequest(HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue)
        {
            supportSessionIDReissue = true;
            return context.Response.IsRequestBeingRedirected;
        }

        public void RemoveSessionID(HttpContext context)
        {
            context.Response.Headers.Remove(sessionKey);
        }

        public void SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded)
        {
            redirected = false;
            cookieAdded = true;
            context.Response.Headers.Add(sessionKey, id);
        }

        public bool Validate(string id)
        {
            Guid parsedUUID = Guid.Empty;
            bool parseSuccess = Guid.TryParse(id, out parsedUUID);
            return parseSuccess && id.ToLower() == parsedUUID.ToString("N").ToLower();
        }
    }
}
