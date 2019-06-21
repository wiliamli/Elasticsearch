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
    /// 小组信息表
    /// </summary>
    [Table("TeamInfo")]
    public class Team : BaseEntity
    {
        /// <summary>
        /// 小组编号
        /// </summary>
        [Required]
        [StringLength(36)]
        public string TeamNumber { get; set; }

        /// <summary>
        /// 小组标识
        /// </summary>
        [Required]
        [StringLength(16)]
        public string TeamCode { get; set; }

        /// <summary>
        /// 小组Leader姓名
        /// </summary>
        [Required]
        [StringLength(16)]
        public string LeaderName { get; set; }

        /// <summary>
        /// 小组Leader工号
        /// </summary>
        [Required]
        [StringLength(16)]
        public string LeaderEmployeeId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(16)]
        public string Password { get; set; }

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
