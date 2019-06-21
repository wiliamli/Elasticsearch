using Jwell.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.MessageQueue.Redis
{
    [Singleton]
    public interface IRedisMQ
    {
        /// <summary>
        /// 向队列中PUSH值
        /// </summary>
        /// <param name="channel">频道</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        bool Publish<T>(string channel, T message);

        /// <summary>
        /// 用于订阅给定的一个频道的信息。
        /// </summary>
        /// <param name="channel">频道</param>
        /// <param name="action">第一个参数是频道，第二个参数是message，这里会自动触发消息</param>
        /// <returns></returns>
        void Subscribe(string channel,Action<string, string> action);

        /// <summary>
        /// 退订给定的频道
        /// </summary>
        /// <param name="channel">频道</param>
        void Unsubscribe(string channel);

        /// <summary>
        /// 全部取消订阅
        /// </summary>
        void UnsubscribeAll();
    }
    
}
