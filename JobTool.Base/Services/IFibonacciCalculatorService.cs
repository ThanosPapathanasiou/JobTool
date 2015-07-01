using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTool.Base.Interfaces;

namespace JobTool.Base.Services
{
    public interface IFibonacciCalculatorService : IService
    {
        long CalculateNthFibonacciNumber(int n);
    }
}
