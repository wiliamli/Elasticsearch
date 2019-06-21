using Jwell.Framework.Mvc;
using System;
using System.Linq;
using System.Web.Http;

namespace Jwell.FullTextSearch.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult StandardAction(Action action)
        {
            //var result = new StandardJsonResult();
            //result.StandardAction(action);
            //return result;

            var result = new StandardJsonResult();
            if (ModelState.IsValid)
            {
                result.StandardAction(action);
            }
            else
            {
                result.Success = false;
                result.Message = ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public StandardJsonResult<T> StandardAction<T>(Func<T> action) 
        {
            //var result = new StandardJsonResult<T>();
            //result.StandardAction(() =>
            //{
            //    result.Data = action();
            //});
            //return result;

            var result = new StandardJsonResult<T>();
            if (ModelState.IsValid)
            {
                result.StandardAction(() =>
                {
                    result.Data = action();
                });
            }
            else
            {
                result.Success = false;
                result.Message = ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage;
            }
            return result;
        }
    }
}