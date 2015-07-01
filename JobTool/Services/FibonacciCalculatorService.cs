using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTool.Base.Services;

namespace JobTool.Services
{
    public class FibonacciCalculatorService: IFibonacciCalculatorService
    {
        public long CalculateNthFibonacciNumber(int n)
        {
            if(n == null)
                throw new ArgumentNullException("n cannot be null");
            if(n <= 0)
                throw new ArgumentOutOfRangeException("n was expected to be over 1");

            if (n < 3)
                return 1;

            return CalculateNthFibonacciNumber(n-1) + CalculateNthFibonacciNumber(n-2);
        }
    }
}
