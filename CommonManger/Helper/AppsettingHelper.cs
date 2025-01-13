using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class AppSettingHelper
    {
        private static IConfiguration _config;

        public AppSettingHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// 读取指定节点的字符串
        /// AppSettingHelper.ReadAppSettings("Test", "A");
        /// </summary>
        /// <param name="sessions"></param>
        /// <returns></returns>
        public static string ReadAppSettings(params string[] sessions)
        {
            try
            {
                if (sessions.Any())
                {
                    return _config[string.Join(":", sessions)];
                }
            }
            catch
            {
                return "";
            }
            return "";
        }
        /// <summary>
        /// 读取实体信息
        /// AppSettingHelper.ReadAppSettings<CustomConnectionConfig>("MasterSlaveConnectionStrings");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ReadAppSettings<T>(string key) where T : new()
        {
            var re = new T();
            _config.GetSection(key).Bind(re);
            return re;
        }
    }
}
