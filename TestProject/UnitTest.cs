using BusinessManager.IService.IBasicService;
using BusinessManager.Service.BasicService;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestProject
{
    public class UnitTest
    {
        protected ITestOutputHelper testOutputHelper;
        public UnitTest(ITestOutputHelper _testOutputHelper)
        {
            testOutputHelper = _testOutputHelper;
        }
        /// <summary>
        /// 简单
        /// </summary>
        [Fact]
        public void Test1()
        {
            // Arrange
            var calculator = new MathCalculator();
            // Act
            var result = calculator.Add(3, 5);
            testOutputHelper.WriteLine("The result is " + result);
            // Assert
            Assert.Equal(8, result);

        }
        /// <summary>
        /// 多个传值测试
        /// </summary>
        /// <param name="value"></param>
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            // Arrange
            var _primeService = new PrimeService();
            var result = _primeService.IsPrime(value);

            Assert.False(result, $"{value} should not be prime");
        }
        public static IEnumerable<object[]> GetComplexTestData()
        {
            yield return new object[] { 10, 5, 15 }; // 测试数据 1
            yield return new object[] { -3, 7, 4 }; // 测试数据 2
            yield return new object[] { 0, 0, 0 }; // 测试数据 3
                                                   // 可以根据需要继续添加更多的测试数据
        }

        /// <summary>
        /// 多个参数传值测试
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="sum"></param>
        [Theory]
        [MemberData(nameof(GetComplexTestData))]
        public void Add_TwoNumbers_ReturnsSumofNumbers01(int first, int second, int sum)
        {
            // Arrange
            var calculator = new MathCalculator();

            // Act
            var result = calculator.Add(first, second);

            // Assert
            Assert.Equal(sum, result);
        }
        /// <summary>
        /// 自定义对象多参数传值测试
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="expectedSum"></param>
        [Theory]
        [CustomData(1, 2, 3)]
        [CustomData(2, 3, 5)]
        public void Add_TwoNumbers_ReturnSum03(int num1, int num2, int expectedSum)
        {
            // Arrange
            var calculator = new MathCalculator();

            // Act
            var result = calculator.Add(num1, num2);

            // Assert
            Assert.Equal(expectedSum, result);
        }
        [Fact]
        public void TestOutPut()
        {
            // Arrange
            var calculator = new MathCalculator();

            // Act
            var result = calculator.Add(3, 5);
            testOutputHelper.WriteLine("The result is " + result);
            // Assert
            Assert.Equal(8, result);

        }
    }
}