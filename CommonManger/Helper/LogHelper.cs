namespace CommonManager.Helper
{
    public class LogHelper
    {
        private static readonly log4net.ILog LInfo = log4net.LogManager.GetLogger("LogInfo");

        private static readonly log4net.ILog LError = log4net.LogManager.GetLogger("LogError");

        //private static readonly log4net.ILog LMonitor = log4net.LogManager.GetLogger("LogMonitor");

        //private static readonly log4net.ILog LDebug = log4net.LogManager.GetLogger("LogDebug");

        private static readonly log4net.ILog MysqlLog = log4net.LogManager.GetLogger("MysqlLog");
        /// <summary>
        /// 记录Error日志
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <param name="ex"></param>
        public static void Error(string errorMsg, Exception ex = null)
        {
            if (ex != null)
            {
                LError.Error(errorMsg, ex);
            }
            else
            {
                LError.Error(errorMsg);
            }
        }

        public static void MysqlError(string errorMsg, Exception ex = null)
        {
            if (ex != null)
            {
                MysqlLog.Error(errorMsg, ex);
            }
            else
            {
                MysqlLog.Error(errorMsg);
            }
        }
        public static void MysqlInfo(string errorMsg, Exception ex = null)
        {
            if (ex != null)
            {
                MysqlLog.Info(errorMsg, ex);
            }
            else
            {
                MysqlLog.Info(errorMsg);
            }
        }
        /// <summary>
        /// 记录Info日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Info(string msg, Exception ex = null)
        {
            if (ex != null)
            {
                LInfo.Info(msg, ex);
            }
            else
            {
                LInfo.Info(msg);
            }
        }

        /// <summary>
        /// 记录Monitor日志
        /// </summary>
        /// <param name="msg"></param>
        //public static void Monitor(string msg)
        //{
        //    LMonitor.Info(msg);
        //}

        /// <summary>
        /// 记录Debug日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string msg, Exception ex = null)
        {
            if (ex != null)
            {
                LInfo.Debug(msg, ex);
            }
            else
            {
                LInfo.Debug(msg);
            }
        }

    }

}
