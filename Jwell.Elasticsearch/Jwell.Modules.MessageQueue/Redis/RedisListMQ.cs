using Jwell.Modules.MessageQueue.Redis.Manager;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.MessageQueue.Redis
{
    public class RedisListMQ: IRedisListMQ
    {
        IDatabase Database { get; set; }

        public RedisListMQ()
        {
            Database = RedisManagement.RedisClient.GetDatabase(15);
        }

        public long RPush(string key, string value)
        {
           return Database.ListRightPush(key,value);
        }

        public long LPush(string key, string value)
        {
            return Database.ListLeftPush(key, value);
        }

        public string LPOP(string key)
        {
            return Database.ListLeftPop(key);
        }

        public string RPOP(string key)
        {
            return Database.ListRightPop(key);
        }

        public long ListLength(string key)
        {
            return Database.ListLength(key);
        }
    }
}
