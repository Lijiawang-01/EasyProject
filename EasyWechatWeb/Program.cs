using CommonManager.Helper;
using CommonManager.Init;
using CommonManager.Utity;
using CommonManager.SwaggerExtend;
using AutoMapper;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CommonManager.Cache;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json;
using CommonManager.SqlSugar;
using Autofac.Extras.DynamicProxy;
using CommonManager.Error;
using System.Net;
using CommonManager.SignalR;

namespace EasyWechatWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.InitService();
        }
    }
}