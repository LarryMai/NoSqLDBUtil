using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlDBUtil
{
    /// <summary>
    /// 
    /// </summary>
    public enum StoreDBType
    {
        INFLUXDB = 0,
        //MONGO=1
    }


    public enum ConnectionType
    {
        TCP,
        HTTP,
        HTTPS
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConnectionSetting
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConnectionType Type { get; set; } = ConnectionType.HTTP;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "ip")]
        public string IP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "port")]
        public int Port { get; set; } = 8086;

        /// <summary>
        ///  The property is used for InfluxDB
        /// </summary>
        [JsonProperty("user")]
        public string User { get; set; } = string.Empty;

        /// <summary>
        ///  The property is used for InfluxDB
        /// </summary>
        [JsonProperty("passwd")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        ///  The property is used for InfluxDB
        /// </summary>
        [JsonProperty("db_name")]
        public string DatabaseName { get; set; } = string.Empty;
    }

    public class ConfigSetting
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StoreDBType? DbType { get; set; } = null;

        /// <summary>
        /// The property is used for InfluxDB
        /// </summary>
        [JsonProperty("conn")]
        public ConnectionSetting Connection { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("enable")]
        public bool Enabled { get; set; } = true;
    }
}
