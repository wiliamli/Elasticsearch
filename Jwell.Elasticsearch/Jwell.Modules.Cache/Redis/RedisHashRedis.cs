using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jwell.Modules.Cache.Redis
{
    public sealed class RedisHashRedis
    {
        private static readonly object objLock = new object();
        private IDatabase Database { get; set; }
        public RedisHashRedis(int db = 0)
        {
            //db默认数量
            if (db >= 0 && db < 16)
            {
                Database = RedisManagement.RedisClient.GetDatabase(db);
            }
            else
            {
                throw new ArgumentOutOfRangeException("db", db, "必须在[0,15]的范围");
            }
        }

        /// <summary>
        /// 设置hash
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="hash">哈希值</param>
        public void SetHRang(string hashId, ConcurrentDictionary<string, string> hash)
        {
            lock (objLock) //线程安全
            {
                List<HashEntry> hashEntries = new List<HashEntry>();

                foreach (KeyValuePair<string,string> item in hash)
                {
                    hashEntries.Add(new HashEntry(item.Key,item.Value));
                }

                Database.HashSet(hashId, hashEntries.ToArray());
            }
        }

        /// <summary>
        /// 设置hash表的字段值
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        public void SetHV(string hashId,string key,string value)
        {
            lock (objLock) //线程安全
            {
                Database.HashSet(hashId, key, value);
            }
        }

        /// <summary>
        /// 根据hashId和字段名获取对应字段值
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="key">字段名</param>
        /// <returns></returns>
        public string GetValueFromHash(string hashId, string key)
        {
           return Database.HashGet(hashId, key);
        }

        /// <summary>
        /// 根据hashId获取哈希表
        /// </summary>
        /// <param name="hashId">哈希Id</param>
        /// <returns></returns>
        public Dictionary<string, string> GetHRang(string hashId)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            HashEntry[] hashEntries = Database.HashGetAll(hashId);

            foreach (HashEntry item in hashEntries)
            {
                keyValues.Add(item.Name,item.Value);
            }
            return keyValues;
        }

        /// <summary>
        /// 根据hashId获取字段集合
        /// </summary>
        /// <param name="hashId">哈希Id</param>
        /// <returns>字段集合</returns>
        public List<string> GetHKeys(string hashId)
        {
            return RedisValueToList(Database.HashKeys(hashId));
        }

        /// <summary>
        /// 根据hashId获取字段值集合
        /// </summary>
        /// <param name="hashId">哈希Id</param>
        /// <returns>字段值集合</returns>
        public List<string> GetHValues(string hashId)
        {
            return RedisValueToList(Database.HashValues(hashId));
        }

        /// <summary>
        /// 根据hashId和字段名称移除对应字段值
        /// </summary>
        /// <param name="hashId">哈希Id</param>
        /// <param name="key">字段名</param>
        /// <returns></returns>
        public bool RemoveHByKey(string hashId, string key)
        {
            return Database.HashDelete(hashId,key);
        }

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="keys">字段集合</param>
        /// <returns></returns>
        public bool ReomveHRange(string hashId, IEnumerable<string> keys)
        {
            bool success = false;

            lock (objLock) //线程安全
            {
                foreach (string key in keys)
                {
                    success = Database.HashDelete(hashId, key);
                }
            }
            return success;
        }

        /// <summary>
        /// 是否存在集体的哈希值
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="key">字段名</param>
        /// <returns></returns>
        public bool IsExistHV(string hashId, string key)
        {
            return Database.HashExists(hashId,key);
        }

        /// <summary>
        /// 是否存在Hash表
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <returns></returns>
        public bool IsExistH(string hashId)
        {
            return Database.HashLength(hashId) > 0;
        }

        private List<string> RedisValueToList(RedisValue[] redisValues)
        {
            List<string> list = new List<string>();

            foreach (RedisValue item in redisValues)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
