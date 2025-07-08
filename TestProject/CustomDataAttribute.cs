using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace TestProject
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomDataAttribute : DataAttribute
    {

        private readonly int _first;
        private readonly int _second;
        private readonly int _sum;

        public CustomDataAttribute(int first, int second, int sum)
        {
            _first = first;
            _second = second;
            _sum = sum;
        }
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { _first, _second, _sum };
        }
    }
}
