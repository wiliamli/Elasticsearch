using Jwell.Framework.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.MessageQueue.Redis
{
    [Singleton]
    public interface IRedisListMQ
    {

        long RPush(string key, string value);

        long LPush(string key, string value);

        string LPOP(string key);

        string RPOP(string key);

        long ListLength(string key);
    }
}
