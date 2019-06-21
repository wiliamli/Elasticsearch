using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Jwell.FullTextSearch
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandleAttribute: ExceptionFilterAttribute
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}