using CommonManager.Helper;
using EasyWechatModels.Other;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.SqlSugar
{
    /// <summary>
    /// SqlSugar初始化
    /// 注意配置MasterSlaveConnectionStrings；IsWriteLog
    /// </summary>
    public static class SqlSugarInit
    {
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var options = AppSettingHelper.ReadAppSettings<List<CustomConnectionConfig>>("MasterSlaveConnectionStrings");
            bool isWriteLog = AppSettingHelper.ReadAppSettings("IsWriteLog").ObjToBool();
            List<CustomConnectionConfig> customConnectionConfigList = options;
            List<ConnectionConfig> configList = new List<ConnectionConfig>();
            int id = 0;
            foreach (var customConnectionConfig in customConnectionConfigList)
            {
                var config = new ConnectionConfig()
                {
                    ConfigId = id,
                    ConnectionString = customConnectionConfig.ConnectionString,//也可用127.0.0.1替换localhost
                    DbType = customConnectionConfig.DbType,//设置数据库类型
                    IsAutoCloseConnection = true, //customConnectionConfig.IsAutoCloseConnection,//自动释放数据务，如果存在事务，在事务结束后释放
                     //InitKeyType = InitKeyType.Attribute, //从实体特性中读取主键自增列信息
                    SlaveConnectionConfigs = customConnectionConfig.SlaveConnectionStrings.Select(x => new SlaveConnectionConfig()
                    {
                        ConnectionString = x.ConnectionString,
                        HitRate = x.HitRate,
                    }).ToList()
                };
                id++;
                configList.Add(config);
                LogHelper.Info(config.ToJson());
            }
            services.AddScoped<ISqlSugarClient>(o =>
            {
                var client = new SqlSugarScope(configList);
                //强制查询从库(注意：事务中还是会查主库)
                //db.SlaveQueryable<Order>().ToList();//用法和Queryable一样、
                //默认查主库
                //db.Queryable<Order>().ToList();
                client.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
                {
                    if (isWriteLog)
                    {
                        //获取执行后的总毫秒数
                        double sqlExecutionTotalMilliseconds = client.Ado.SqlExecutionTime.TotalMilliseconds;
                        LogHelper.MysqlInfo(sqlExecutionTotalMilliseconds.ToString());
                    }
                };
                client.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件。可在这里查看生成的sql
                {
                    if (isWriteLog)
                    {
                        string tempSQL = LookSQL(sql, pars);
                        LogHelper.MysqlInfo(tempSQL);
                    }
                };
                client.Aop.OnError = (exp) =>//执行SQL 错误事件
                {
                    if (isWriteLog)
                    {
                        //exp.sql exp.parameters 可以拿到参数和错误Sql  
                        StringBuilder sb_SugarParameterStr = new StringBuilder("###SugarParameter参数为:");
                        var parametres = exp.Parametres as SugarParameter[];
                        if (parametres != null)
                        {
                            sb_SugarParameterStr.Append(JsonConvert.SerializeObject(parametres));
                        }

                        StringBuilder sb_error = new StringBuilder();
                        sb_error.AppendLine("SqlSugarClient执行sql异常信息:" + exp.Message);
                        sb_error.AppendLine("###赋值后sql:" + LookSQL(exp.Sql, parametres));
                        sb_error.AppendLine("###带参数的sql:" + exp.Sql);
                        sb_error.AppendLine("###参数信息:" + sb_SugarParameterStr.ToString());
                        sb_error.AppendLine("###StackTrace信息:" + exp.StackTrace);

                        LogHelper.MysqlError(sb_error.ToString());
                    }

                };
                client.Aop.OnDiffLogEvent = (exp) =>
                {

                };
                client.Aop.OnExecutingChangeSql = (sql, pars) => //SQL执行前 可以修改SQL
                {
                    //判断update或delete方法是否有where条件。如果真的想删除所有数据，请where(p=>true)或where(p=>p.id>0)
                    if (sql.TrimStart().IndexOf("delete ", StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        if (sql.IndexOf("where", StringComparison.CurrentCultureIgnoreCase) <= 0)
                        {
                            throw new Exception("delete删除方法需要有where条件！！");
                        }
                    }
                    else if (sql.TrimStart().IndexOf("update ", StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        if (sql.IndexOf("where", StringComparison.CurrentCultureIgnoreCase) <= 0)
                        {
                            throw new Exception("update更新方法需要有where条件！！");
                        }
                    }

                    return new KeyValuePair<string, SugarParameter[]>(sql, pars);
                };

                return client;
            });

            /// <summary>
            /// 查看赋值后的sql
            /// </summary>
            /// <param name="sql"></param>
            /// <param name="pars">参数</param>
            /// <returns></returns>
            static string LookSQL(string sql, SugarParameter[] pars)
            {
                if (pars == null || pars.Length == 0) return sql;

                StringBuilder sb_sql = new StringBuilder(sql);
                var tempOrderPars = pars.Where(p => p.Value != null).OrderByDescending(p => p.ParameterName.Length).ToList();//防止 @par1错误替换@par12
                for (var index = 0; index < tempOrderPars.Count; index++)
                {
                    sb_sql.Replace(tempOrderPars[index].ParameterName, "'" + tempOrderPars[index].Value.ToString() + "'");
                }

                return sb_sql.ToString();
            }
        }

    }
}
