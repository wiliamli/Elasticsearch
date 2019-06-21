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
using System.Web.Http;

namespace Jwell.Sample.Controllers
{
    public class IndexManagerController : BaseApiController
    {
        private IIndexManagerService IndexManagerService { get; set; }

        public IndexManagerController(IIndexManagerService indexManagerService)
        {
            this.IndexManagerService = indexManagerService;
        }

        // GET: api/IndexManager
        public StandardJsonResult<PageResult<IndexListDto>> GetPage([FromUri]SearchIndexParams param)
        {
            return StandardAction(() => this.IndexManagerService.GetIndexByPage(param));
        }

        // GET: api/IndexManager/5
        public StandardJsonResult<IndexDto> GetIndex(string indexNumber)
        {
            return StandardAction(() => this.IndexManagerService.GetOne(indexNumber));
        }

        // POST: api/IndexManager
        public StandardJsonResult PostIndex([FromBody]IndexAddParam param)
        {
            string errorMsg = string.Empty;
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
            {
                isSuccess = this.IndexManagerService.AddIndex(param, ref errorMsg);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "添加成功" : (string.IsNullOrEmpty(errorMsg) ? "添加失败" : errorMsg);
            return standardJsonResult;
        }

        // PUT: api/IndexManager/5
        public StandardJsonResult Put(string indexNumber, [FromBody]IndexModifyParam param)
        {
            bool isSuccess = false;
            string errorMsg = string.Empty;
            var standardJsonResult = StandardAction(() =>
            {
                isSuccess = this.IndexManagerService.Modify(indexNumber, param, ref errorMsg);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "修改成功" : string.IsNullOrEmpty(errorMsg) ? "修改失败" : errorMsg;
            return standardJsonResult;
        }

        // DELETE: api/IndexManager/5
        public StandardJsonResult Delete(string indexNumber)
        {
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
            {
                isSuccess = this.IndexManagerService.Delete(indexNumber);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "删除成功" : "删除失败";
            return standardJsonResult;
        }

        public StandardJsonResult<IndexDetailDto> GetIndexDetail(string indexNumber)
        {
            return StandardAction(() => this.IndexManagerService.GetIndexDetail(indexNumber));
        }

        [HttpGet]
        public StandardJsonResult StartIndex(string indexNumber)
        {
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
            {
                isSuccess = this.IndexManagerService.StartIndex(indexNumber);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "启用成功" : "启用失败";
            return standardJsonResult;
        }

        [HttpGet]
        public StandardJsonResult StopIndex(string indexNumber)
        {
            bool isSuccess = false;
            var standardJsonResult = StandardAction(() =>
            {
                isSuccess = this.IndexManagerService.StopIndex(indexNumber);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "停用成功" : "停用失败";
            return standardJsonResult;
        }

        [HttpGet]
        public StandardJsonResult ExpensionIndex(string indexNumber)
        {
            bool isSuccess = false;
            string errorMsg = string.Empty;
            var standardJsonResult = StandardAction(() =>
            {
                isSuccess = this.IndexManagerService.Expension(indexNumber, ref errorMsg);
            });

            standardJsonResult.Success = isSuccess;
            if (string.IsNullOrEmpty(standardJsonResult.Message))
                standardJsonResult.Message = isSuccess ? "扩容成功" : string.IsNullOrEmpty(errorMsg) ? "扩容失败" : errorMsg;
            return standardJsonResult;
        }
    }
}
