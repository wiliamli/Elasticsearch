﻿using Jwell.Framework.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Configure
{
    public class JwellConfig
    {
        /// <summary>
        /// 根据键获取本地对应的配置信息
        /// </summary>
        /// <param name="key">对应的键值</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 根据键获取统一配置信息
        /// </summary>
        /// <param name="key">对应的键值</param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            throw new NotImplementedException();
        }
    }
}
