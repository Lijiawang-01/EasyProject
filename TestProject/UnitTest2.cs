using Autofac;
using BusinessManager.IService.IBasicService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestProject
{
    /// <summary>
    /// 使用方法auto注册依赖注入
    /// </summary>
    public class UnitTest2
    {
        protected IContainer conllections;
        protected ITestOutputHelper testOutputHelper;
        public UnitTest2(ITestOutputHelper _testOutputHelper)
        {
             conllections = CommInject.InitIoc();
            testOutputHelper = _testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            var userService = conllections.Resolve<IUserService>();
            // Act
            var user = userService.GetList();
            var result = user.FirstOrDefault()?.Name;
            testOutputHelper.WriteLine("The result is " + result);
            // Assert
            Assert.Equal("admin", result);

        }
    }
}
