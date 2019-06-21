using Jwell.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jwell.FullTextSearch.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        // GET: Base

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public StandardJsonResult StandardAction(Action action)
        {
            var result = new StandardJsonResult();
            result.StandardAction(action);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public StandardJsonResult<T> StandardAction<T>(Func<T> action)
        {
            var result = new StandardJsonResult<T>();
            result.StandardAction(() =>
            {
                result.Data = action();
            });
            return result;
        }

        /// <summary>
        /// 流文件下载
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileStream">文件流</param>
        public void DownloadFile(string fileName, byte[] fileStream)
        {
            Response.ContentType = "application/ms-excel";
            Response.AddHeader(@"Content-Disposition", string.Format("attachment; filename={0}",
                System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)));
            Response.BinaryWrite(fileStream);
            Response.Flush();
            Response.End();
        }
    }
}