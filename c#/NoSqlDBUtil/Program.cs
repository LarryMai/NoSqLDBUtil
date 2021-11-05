using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace NoSqlDBUtil
{
    partial class Program
    {
        /// <summary>
        /// 
        /// </summary>
        static ConfigSetting _setting = null;

        /// <summary>
        /// 
        /// </summary>
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger(typeof(Program));

        /// <summary>
        /// 
        /// </summary>
        static void Main(string[] args)
        {
            string configPath = Program.ConfigFilePath;
            if (configPath.AsConfig<ConfigSetting>(out _setting))
            {
                _logger.Info($"load {configPath} failed");
            }


        }
    }

    public static class StringExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger(typeof(StringExtensions));

        /// <summary>
        /// 
        /// </summary>
        public static bool AsConfig<T>(this string filePath, out T deviceConfig)
        where T : class
        {
            deviceConfig = default(T);
            bool res = false;
            try
            {
                string json = File.ReadAllText(filePath);
                deviceConfig = JsonConvert.DeserializeObject<T>(json);
                res = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool AsConfig<T>(this string filePath, string sectionName, out T deviceConfig)
        where T : class
        {
            deviceConfig = default(T);
            bool res = false;
            try
            {
                string json = File.ReadAllText(filePath);
                JObject jObj = JObject.Parse(json);
                if (jObj.ContainsKey(sectionName))
                {
                    deviceConfig = JsonConvert.DeserializeObject<T>(jObj[sectionName].ToString(Formatting.Indented));
                    res = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return res;
        }
    }
}