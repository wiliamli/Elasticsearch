using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Configure
{
    public interface IConfigure
    {
        /// <summary>
        /// 根据键获取对应的配置信息
        /// </summary>
        /// <param name="key">对应的键值</param>
        /// <returns></returns>
        string Get(string key);
    }
}
