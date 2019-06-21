using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Params
{
   /// <summary>
   /// 索引修改参数
   /// </summary>
    public class IndexModifyParam
    {
        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// 索引大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 索引分片数量
        /// </summary>
        public int ShardNum { get; set; }

        /// <summary>
        /// 分片副本数量
        /// </summary>
        public int ReplicasNum { get; set; }

        /// <summary>
        /// 最大扩容次数
        /// </summary>
        public int MaxExpensionNum { get; set; }
    }
}
