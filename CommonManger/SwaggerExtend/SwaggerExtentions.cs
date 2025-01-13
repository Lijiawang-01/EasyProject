using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.SwaggerExtend
{
    /// <summary>
    /// Swagger扩展注册
    /// </summary>
    public static class SwaggerExtentions
    {
        /// <summary>
        /// 注册swagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerExt(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //注册Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                #region 配置Swagger
                //版本控制
                foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
                {
                    c.SwaggerDoc(field.Name, new OpenApiInfo()
                    {
                        Title = $"{field.Name} ,这是标题",
                        Version = field.Name,
                        Description = $"{field.Name} ,这是版本内容",
                    });
                }
                #endregion

                #region 配置展示注释
                {
                    var path = Path.Combine(AppContext.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.xml");  // xml文档绝对路径
                    c.IncludeXmlComments(path, true); // true : 显示控制器层注释
                    c.OrderActionsBy(o => o.RelativePath); // 对action的名称进行排序，如果有多个，就可以看见效果了
                }
                #endregion
                #region 配置使用token
                {
                    // 增加安全定义
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "请输入token,格式为 Bearer xxxxx （注意中间有空格）",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    //增加安全要求
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference()
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },new string[]{}
                        }
                    });
                }
                #endregion
            });

        }
        /// <summary>
        /// 使用Swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerExt(this WebApplication app)
        {
            #region 使用Swagger
            //使用Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
                {
                    c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
                }
            });
            #endregion
        }
    }
}
