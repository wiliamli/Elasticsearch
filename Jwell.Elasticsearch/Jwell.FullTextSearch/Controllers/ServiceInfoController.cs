using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Mvc;
using Jwell.Framework.Paging;
using Jwell.FullTextSearch.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Jwell.Sample.Controllers
{
    public class ServiceInfoController : BaseApiController
    {
        private IServiceInfoService ServiceInfoService { get; set; }

        public ServiceInfoController(IServiceInfoService serviceInfoService)
        {
            this.ServiceInfoService = serviceInfoService;
        }

        // GET: api/ServiceInfo
        public StandardJsonResult<PageResult<ServiceInfoDto>> Get([FromUri]SearchServiceInfoParams param)
        {
            var teamNum = (string)HttpContext.Current.Session["loginUser"];
            return StandardAction(() => ServiceInfoService.GetListByPage(param, teamNum));
        }


        // POST: api/ServiceInfo
        public StandardJsonResult Post([FromBody]ServiceInfoAddParams param)
        {
            string errorMsg = string.Empty;
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
            {
                var teamNumber = (string)HttpContext.Current.Session["loginUser"];
                isSuccess = this.ServiceInfoService.Add(param, teamNumber, ref errorMsg);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "添加成功" : (string.IsNullOrEmpty(errorMsg) ? "添加失败" : errorMsg);
            return standardJsonResult;
        }
    }
}
