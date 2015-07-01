using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JobTool.Base;
using JobTool.Base.Interfaces.Services;
using JobTool.Base.Services;

namespace JobTool.Commands
{
    public class CalculateFibonacciCommand: ICommand, IHelp
    {
        private readonly IUserInterfaceService ui;
        private readonly IFibonacciCalculatorService fibonacciCalculator;

        public CalculateFibonacciCommand(IUserInterfaceService ui, IFibonacciCalculatorService fibonacciCalculator)
        {
            this.ui = ui;
            this.fibonacciCalculator = fibonacciCalculator;
        }

        public void Help()
        {
            ui.Output("This command calculates the Nth fibonacci number, where N is a positive integer greater than zero");
        }

        public void Execute(object parameter)
        {
            var n = Convert.ToInt32(parameter);
            var fibonacci = fibonacciCalculator.CalculateNthFibonacciNumber(n);
            ui.Output(String.Format("The {0}th fibonacci number is {1}", n, fibonacci));
        }

        public bool CanExecute(object parameter)
        {
            if(parameter == null)
                return false;

            try
            {
                int n = Convert.ToInt32(parameter);

                if(n <= 0)
                    return false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
