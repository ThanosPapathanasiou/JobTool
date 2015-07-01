using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTool.Base.Services;

namespace JobTool.Services
{
    public class MemoizedCalculateFibonacciService: IFibonacciCalculatorService
    {
        private long[] memoize;
        private int current;

        public MemoizedCalculateFibonacciService()
        {
            memoize = new long[32];
            memoize[0] = 0;
            memoize[1] = 1;
            memoize[2] = 1;
            current = 2;
        }

        public long CalculateNthFibonacciNumber(int n)
        {
            if(n == null)
                throw new ArgumentNullException("n cannot be null");
            if(n <= 0)
                throw new ArgumentOutOfRangeException("n was expected to be over 1");

            if (n <= current)
                return memoize[n];

            Array.Resize(ref memoize, n + 1);

            for (int i = current + 1; i <= n; i++)
            {
                memoize[i] = memoize[i - 2] + memoize[i - 1];
            }

            current = n;
            return memoize[current];
        }
    }
}
