using SqlSugar;

namespace EasyWechatModels.Other
{
    /// <summary>
    /// 自定义从库连接字符串
    /// </summary>
    public class CustomSlaveConnectionString : SlaveConnectionConfig
    {
        private int _CustomHitRate;
        public int CustomHitRate
        {
            get { return _CustomHitRate; }
            set
            {
                _CustomHitRate = value;
                HitRate = value;
            }
        }
    }
}
