using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    /// <summary>
    /// Consul注册
    /// 注意配置ConsulServiceAddress；WebApiPort；ConsulHealthUrl
    /// </summary>
    public static class ConsulHelper
    {
        public static void ConsulRegist(this IConfiguration configuration, string name = "EpsConsulService")
        {
            string consulServiceAddress = AppSettingHelper.ReadAppSettings("ConsulServiceAddress");
            if (consulServiceAddress + "" != "")
            {
                try
                {
                    //WebApiPort--  http://*:5000
                    string port = AppSettingHelper.ReadAppSettings("WebApiPort");//configuration["port"];
                    var portArr = port.Split(';');
                    port = port.ToLower().Replace("http://", "");
                    int portIndex = port.LastIndexOf(":");

                    //webapi ip地址
                    string ip = port.ToLower().Substring(0, portIndex);// configuration["ip"];
                    if (ip == "*")
                    {
                        ip = "127.0.0.1";
                    }

                    //webapi端口
                    port = port.Substring(portIndex).TrimStart(':').TrimEnd(';');

                    //权重，默认3
                    string weight = "3";//configuration["weight"];

                    //consul服务地址
                    string consulAddress = consulServiceAddress; 
                    //consul服务名称
                    string consulCenter = "dc1";
                    ConsulClient client = new ConsulClient(c =>
                    {
                        c.Address = new Uri(consulAddress);
                        c.Datacenter = consulCenter;
                    });

                    //consul心跳地址，可配置
                    string ConsulHealthUrl = AppSettingHelper.ReadAppSettings("ConsulHealthUrl");
                    if (String.IsNullOrEmpty(ConsulHealthUrl))
                    {
                        ConsulHealthUrl = "/Api/Health/Index";
                    }

                    client.Agent.ServiceRegister(new AgentServiceRegistration()
                    {
                        ID = "service " + ip + ":" + port,//Ray--唯一的
                        Name = name,//分组 
                        Address = ip,
                        Port = int.Parse(port),
                        Tags = new string[] { weight.ToString() },//额外标签信息
                        Check = new AgentServiceCheck()
                        {
                            Interval = TimeSpan.FromSeconds(12),//心跳评率
                            HTTP = $"http://{ip}:{port}{ConsulHealthUrl}",
                            Timeout = TimeSpan.FromSeconds(5),
                            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                        }//配置心跳
                    });
                    Console.WriteLine($"Consul微服务注册：{ip}:{port}--weight:{weight}，服务名：{name}，Consul地址：{consulServiceAddress}"); //命令行参数获取
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Consul微服务注册异常：{ex.Message}");
                }
            }
        }
    }
}
