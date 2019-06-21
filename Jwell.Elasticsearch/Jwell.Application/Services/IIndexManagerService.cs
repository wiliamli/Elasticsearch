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
    /// 索引管理-服务-接口
    /// </summary>
    public interface IIndexManagerService : IApplicationService
    {
        /// <summary>
        /// 获取索引分页数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PageResult<IndexListDto> GetIndexByPage(SearchIndexParams param);

        /// <summary>
        /// 获取一条索引信息
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        IndexDto GetOne(string indexNumber);

        /// <summary>
        /// 添加索引
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool AddIndex(IndexAddParam param, ref string errorMsg);

        /// <summary>
        /// 修改索引
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Modify(string indexNumber, IndexModifyParam param, ref string errorMsg);

        /// <summary>
        /// 启用索引
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        bool StartIndex(string indexNumber);

        /// <summary>
        /// 停用索引
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        bool StopIndex(string indexNumber);

        /// <summary>
        /// 索引扩容
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        bool Expension(string indexNumber, ref string errorMsg);

        /// <summary>
        /// 获取索引详情
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        IndexDetailDto GetIndexDetail(string indexNumber);

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="IndexNumber"></param>
        /// <returns></returns>
        bool Delete(string IndexNumber);
    }
}
