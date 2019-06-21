using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 服务信息管理-服务-接口
    /// </summary>
    public interface IServiceInfoService : IApplicationService
    {
        /// <summary>
        /// 获取服务信息分页列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PageResult<ServiceInfoDto> GetListByPage(SearchServiceInfoParams param, string teamNumber);

        /// <summary>
        /// 添加服务信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Add(ServiceInfoAddParams dto, string teamNumber, ref string errorMsg);
    }
}
