using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwell.Modules.Cache.Redis;

namespace Jwell.Modules.Cache
{
    public class HCacheClient : IHCacheClient
    {
        private int db = 0;
        public int DB
        {
            get
            {
                return db;
            }
            set
            {
                if (value >= 0 && value < 16)
                    db = value;
                else
                    throw new ArgumentOutOfRangeException("db", db, "必须在[0,15]的范围");
            }
        }

        private RedisHashRedis RedisHashCache => new RedisHashRedis(DB);

       
        public IEnumerable<string> GetHKeys(string hashId)
        {
            return RedisHashCache.GetHKeys(hashId);
        }

        public IEnumerable<KeyValuePair<string, string>> GetHT(string hashId)
        {
            return RedisHashCache.GetHRang(hashId);
        }

        public string GetHV(string hashId, string key)
        {
            return RedisHashCache.GetValueFromHash(hashId,key);
        }

        public IEnumerable<string> GetHValues(string hashId)
        {
            return RedisHashCache.GetHValues(hashId);
        }

        public bool IsExistH(string hashId)
        {
            return RedisHashCache.IsExistH(hashId);
        }

        public bool IsExistHV(string hashId, string key)
        {
            return RedisHashCache.IsExistHV(hashId,key);
        }

        public bool RemoveHK(string hashId, string key)
        {
            return RedisHashCache.RemoveHByKey(hashId,key);
        }

        public bool ReomveHKS(string hashId, IEnumerable<string> keys)
        {
            return RedisHashCache.ReomveHRange(hashId, keys);
        }

        public void SetHT(string hashId, ConcurrentDictionary<string, string> hash)
        {
            RedisHashCache.SetHRang(hashId, hash);
        }

        public void SetHV(string hashId, string key, string value)
        {
            RedisHashCache.SetHV(hashId, key,value);
        }
    }
}
