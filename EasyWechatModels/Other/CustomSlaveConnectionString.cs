using SqlSugar;

namespace EasyWechatModels.Other
{
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
