using Jwell.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Domain.Entities
{
    /// <summary>
    /// 索引信息管理表
    /// </summary>
    [Table("IndexManager")]
    public class IndexManager : BaseEntity
    {
        /// <summary>
        /// 服务编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string ServiceNumber { get; set; }

        /// <summary>
        /// 服务标识
        /// </summary>
        [Required]
        [StringLength(32)]
        public string ServiceSign { get; set; }

        /// <summary>
        /// 索引名称
        /// </summary>
        [Required]
        [StringLength(32)]
        public string IndexName { get; set; }

        /// <summary>
        /// 索引编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string IndexNumber { get; set; }

        /// <summary>
        /// 索引大小
        /// </summary>
        [Required]
        public int Size { get; set; }

        /// <summary>
        /// 索引分片数量
        /// </summary>
        [Required]
        public int ShardNumber { get; set; }

        /// <summary>
        /// 分片副本数量
        /// </summary>
        [Required]
        public int ReplicasNumber { get; set; }

        /// <summary>
        /// 已扩容次数
        /// </summary>
        [Required]
        public int ExpensionNum { get; set; }

        /// <summary>
        /// 最大扩容次数
        /// </summary>
        [Required]
        public int MaxExpensionNum { get; set; }

        ///// <summary>
        ///// 索引备注
        ///// </summary>
        //[StringLength(1024)]
        //public string Remark { get; set; }

        /// <summary>
        /// 索引状态
        /// </summary>
        [Required]
        public IndexStatus Status { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Required]
        [StringLength(16)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [Required]
        [StringLength(16)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Required]
        public DateTime ModifiedTime { get; set; }
    }
}
