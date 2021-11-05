using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlDBUtil
{
    partial class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public static string Version
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// The config file path
        /// </summary>
        public static string ConfigFilePath
        {
            get
            {
                const string CONFIG_FILE_NAME = "config.json";
                string currentDir = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;
                return System.IO.Path.Combine(currentDir, CONFIG_FILE_NAME);
            }
        }


        /// <summary>
        /// The nlog file path
        /// </summary>
        public static string NLogFilePath
        {
            get
            {
                const string NLOG_CONFIG_FILE_NAME = "NLog.config";
                string currentDir = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;
                return System.IO.Path.Combine(currentDir, NLOG_CONFIG_FILE_NAME);
            }
        }
    }
}
