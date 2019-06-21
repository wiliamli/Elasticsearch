using Jwell.Framework.Ioc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache
{
    [Singleton]
    public interface IHCacheClient
    {
        /// <summary>
        /// 切换数据库
        /// </summary>
        int DB { get; set; }

        /// <summary>
        /// 设置hash表的字段值
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="key">字段名</param>
        /// <param name="value">字段值</param>
        void SetHV(string hashId, string key, string value);

        /// <summary>
        /// 设置hash
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="hash">哈希值</param>
        void SetHT(string hashId, ConcurrentDictionary<string, string> hash);

        /// <summary>
        /// 根据hashId和字段名获取对应字段值
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="key">字段名</param>
        /// <returns></returns>
        string GetHV(string hashId, string key);

        /// <summary>
        /// 根据hashId获取哈希表
        /// </summary>
        /// <param name="hashId">哈希Id</param>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, string>> GetHT(string hashId);

        /// <summary>
        /// 根据hashId获取字段集合
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        IEnumerable<string> GetHKeys(string hashId);

        /// <summary>
        /// 根据hashId获取字段值集合
        /// </summary>
        /// <param name="hashId">哈希Id</param>
        /// <returns>字段值集合</returns>
        IEnumerable<string> GetHValues(string hashId);

        /// <summary>
        /// 根据hashId和字段名称移除对应字段值
        /// </summary>
        /// <param name="hashId">哈希Id</param>
        /// <param name="key">字段名</param>
        /// <returns></returns>
        bool RemoveHK(string hashId, string key);

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="keys">字段集合</param>
        /// <returns></returns>
        bool ReomveHKS(string hashId, IEnumerable<string> keys);

        /// <summary>
        /// 是否存在集体的哈希值
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <param name="key">字段名</param>
        /// <returns></returns>
        bool IsExistHV(string hashId, string key);

        /// <summary>
        /// 是否存在Hash表
        /// </summary>
        /// <param name="hashId">哈希ID</param>
        /// <returns></returns>
        bool IsExistH(string hashId);
    }
}
