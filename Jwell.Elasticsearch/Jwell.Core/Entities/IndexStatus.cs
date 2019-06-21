using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Domain.Entities
{
    /// <summary>
    /// 索引状态
    /// </summary>
    public enum IndexStatus : byte
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
        Deleted =3,
    }

    public static class IndexStatusExt
    {
        public static string GetDescription(this IndexStatus value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? string.Empty : attribute.Description;
        }
    }
}
