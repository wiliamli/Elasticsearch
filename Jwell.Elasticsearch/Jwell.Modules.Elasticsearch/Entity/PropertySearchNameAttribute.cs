using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Entity
{
    /// <summary>
    /// 搜索字段名称属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertySearchNameAttribute : Attribute
    {
        public PropertySearchNameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
    }
}
