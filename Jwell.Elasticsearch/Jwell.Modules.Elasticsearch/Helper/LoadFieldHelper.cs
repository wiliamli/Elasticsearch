using Jwell.Modules.Elasticsearch.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Helper
{
    /// <summary>
    /// 字段名称获取帮助类
    /// </summary>
    internal class LoadFieldHelper
    {
        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetFiledStr<T, TV>(Expression<Func<T, TV>> expression)
        {
            string filed = string.Empty;
            Expression expr = expression;
            for (; ; )
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)expr;
                        var propertyInfo = memberExpression.Member as PropertyInfo;
                        if (propertyInfo == null)
                            throw new ArgumentNullException(typeof(T).Name);
                        var attribute = propertyInfo.GetCustomAttributes(typeof(PropertySearchNameAttribute), false)
                            .FirstOrDefault();
                        if (attribute == null)
                            throw new ArgumentNullException(typeof(PropertySearchNameAttribute).Name);
                        filed = $"{((PropertySearchNameAttribute)attribute).Name}.{filed}";
                        expr = memberExpression.Expression;
                        break;
                    case ExpressionType.Parameter:
                        return filed.TrimEnd('.');
                    default:
                        return null;
                }
            }
        }

        public static string[] GetProperty(LambdaExpression lambda)
        {
            Expression expr = lambda;
            for (; ; )
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;

                    case ExpressionType.MemberInit:
                        var memberExpression = ((MemberInitExpression)expr);
                        var mi = memberExpression.Bindings.Select(x => GetFiledStr(((MemberAssignment)x).Expression)).ToArray();
                        return mi;
                    case ExpressionType.MemberAccess:
                        var accessExpression = ((MemberExpression)expr);
                        var attribute = accessExpression.Member.GetCustomAttributes(typeof(PropertySearchNameAttribute), false)
                           .FirstOrDefault();
                        if (attribute == null)
                            throw new ArgumentNullException(typeof(PropertySearchNameAttribute).Name);
                        var upper = ((PropertySearchNameAttribute)attribute).Name;
                        var fields = accessExpression.Type.GetProperties();
                        return fields.Select(x => Field(x, upper)).ToArray();
                    default:
                        return null;
                }
            }
        }

        private static string Field(PropertyInfo info, string upper)
        {
            var attribute = info.GetCustomAttributes(typeof(PropertySearchNameAttribute), false)
                           .FirstOrDefault();
            if (attribute == null)
                throw new ArgumentNullException(typeof(PropertySearchNameAttribute).Name);
            return $"{upper}.{((PropertySearchNameAttribute)attribute).Name}";
        }

        private static string GetFiledStr(Expression expression)
        {
            string filed = string.Empty;
            Expression expr = expression;
            for (; ; )
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)expr;
                        var propertyInfo = memberExpression.Member as PropertyInfo;
                        if (propertyInfo == null)
                            throw new ArgumentNullException("属性异常");

                        var attribute = propertyInfo.GetCustomAttributes(typeof(PropertySearchNameAttribute), false)
                            .FirstOrDefault();
                        if (attribute == null)
                            throw new ArgumentNullException(typeof(PropertySearchNameAttribute).Name);
                        filed = $"{((PropertySearchNameAttribute)attribute).Name}.{filed}";
                        expr = memberExpression.Expression;
                        break;
                    case ExpressionType.Parameter:
                        return filed.TrimEnd('.');
                    default:
                        return null;
                }
            }
        }
    }
}
