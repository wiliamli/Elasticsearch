using Jwell.Modules.MessageQueue.Redis.Manager;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.MessageQueue.Redis
{
    public class RedisMQ : IRedisMQ
    {
        private ISubscriber Subscriber { get; set; }

        /// <summary>
        /// 不保证可靠性
        /// </summary>
        public RedisMQ()
        {
            Subscriber = RedisManagement.RedisClient.GetSubscriber();
        }

        public bool Publish<T>(string channel, T message)
        {
            return Subscriber.Publish(channel, Framework.Utilities.Serializer.ToJson(message)) > 0;
        }

        public void Subscribe(string subChannel,Action<string,string> action)
        {
           
            Subscriber.Subscribe(subChannel, (channel, message) =>
            {
                action(channel,message);
            });
        }

        public void Unsubscribe(string channel)
        {
            Subscriber.Unsubscribe(channel);
        }

        public void UnsubscribeAll()
        {
            Subscriber.UnsubscribeAll();
        }
    }
}
