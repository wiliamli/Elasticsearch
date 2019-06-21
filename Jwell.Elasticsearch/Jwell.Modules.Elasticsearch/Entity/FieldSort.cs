using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Entity
{
    /// <summary>
    /// 排序字段
    /// </summary>
    public class FieldSort
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 是否为正序
        /// </summary>
        public bool isAsc { get; set; }
    }
}
