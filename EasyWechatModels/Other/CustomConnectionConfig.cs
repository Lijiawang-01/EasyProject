using SqlSugar;

namespace EasyWechatModels.Other
{
    /// <summary>
    /// 从库连接配置
    /// </summary>
    public class CustomConnectionConfig
    {
        public CustomConnectionConfig()
        {
            SlaveConnectionStrings = new List<CustomSlaveConnectionString>();
        }
        public string? ConnectionString { get; set; }
        //SqlSugar.DbType
        public DbType DbType { get; set; }
        public bool IsAutoCloseConnection { get; set; }
        //InitKeyType
        public InitKeyType Attribute { get; set; }
        public List<CustomSlaveConnectionString> SlaveConnectionStrings { get; set; }
        public bool IsWriteLog { get; set; }
    }
}
