using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Entity
{
    /// <summary>
    /// 索引操作枚举
    /// </summary>
    public enum IndexOperateStatus : byte
    {

        /// <summary>
        /// 未启用
        /// </summary>
        [Description("未启用")]
        NotStarted = 0,

        /// <summary>
        /// 已启用
        /// </summary>
        [Description("已启用")]
        Started = 1,

        /// <summary>
        /// 已停用
        /// </summary>
        [Description("已停用")]
        Stoped = 2,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已停用")]
        Deleted = 3,

        /// <summary>
        /// 启用中
        /// </summary>
        [Description("启用中")]
        Starting = 4
    }
}
