using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class MathCalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
    public class PrimeService
    {
        public bool IsPrime(int a)
        {
            return a > 0;
        }
    }

}
