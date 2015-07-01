using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTool.Base.Services;
using JobTool.Services;
using NUnit.Framework;

namespace JobTool.Tests.Services
{
    public abstract class IFibonacciCalculatorServiceTests
    {
        private IFibonacciCalculatorService calculator;

        [SetUp]
        public void Setup()
        {
            calculator = CreateCalculator();
        }

        protected abstract IFibonacciCalculatorService CreateCalculator();

        [TestCase(-1)]
        [TestCase(0)]
        public void Given_ZeroOrNegativeNumber_Throws_Exception(int n)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNthFibonacciNumber(n));
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(5, 5)]
        public void Given_Number_Returns_Correct_Result(int n, long expected)
        {
            long actual = calculator.CalculateNthFibonacciNumber(n);
            Assert.AreEqual(expected,actual);
        }

    }

    [TestFixture]
    public class FibonacciCalculatorServiceTests : IFibonacciCalculatorServiceTests
    {
        protected override IFibonacciCalculatorService CreateCalculator()
        {
            return new FibonacciCalculatorService();
        }
    }

    [TestFixture]
    public class MemoizedFibonacciCalculatorServiceTests : IFibonacciCalculatorServiceTests
    {
        protected override IFibonacciCalculatorService CreateCalculator()
        {
            return new MemoizedCalculateFibonacciService();
        }
    }
}
