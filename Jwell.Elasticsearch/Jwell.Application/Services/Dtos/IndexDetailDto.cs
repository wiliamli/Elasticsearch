using Jwell.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services.Dtos
{
    /// <summary>
    /// 索引Dto
    /// </summary>
    public class IndexDetailDto
    {
       /// <summary>
       /// 索引名称
       /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// 索引状态
        /// </summary>
        public string Status { get; set; }

        [JsonIgnore]
        public IndexStatus EnumStatus { get; set; }

        /// <summary> 
        /// 索引大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 分片数量
        /// </summary>
        public int ShardNum { get; set; }

        /// <summary>
        /// 副本数量
        /// </summary>
        public int ReplicasNum { get; set; }

        /// <summary>
        /// 已扩容次数
        /// </summary>
        public int ExpentionNum { get; set; }

        /// <summary>
        /// 最大扩容次数
        /// </summary>
        public int MaxExpensionNum { get; set; }
    }
}
