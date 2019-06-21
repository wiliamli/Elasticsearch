using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch.Entity
{
    /// <summary>
    /// Elasticsearch异常
    /// </summary>
    [Serializable]
    internal class ElasticsearchException : Exception
    {
        public ElasticsearchException()
        {
        }

        /// <summary>
        /// Elasticsearch构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public ElasticsearchException(string message)
            : base(message)
        { }

        /// <summary>
        /// Elasticsearch构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public ElasticsearchException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
