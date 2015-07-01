using System;
using System.Windows.Input;
using Castle.MicroKernel;
using JobTool.Base.Services;

namespace JobTool.Service
{
    public class CommandParserService : ICommandParserService
    {
        private readonly IKernel container;

        public CommandParserService(IKernel container)
        {
            this.container = container;
        }


        public Tuple<ICommand, object> Parse(string input)
        {
            ICommand command = null;
            object parameter = null;

            if (input.Contains(" "))
            {
                string input1 = input.Split(' ')[0];
                string input2 = input.Split(' ')[1];

                command = container.Resolve<ICommand>(input1);
                try
                {
                    parameter = container.Resolve<ICommand>(input2);
                }
                catch
                {
                    parameter = input2;
                }
            }
            else
            {
                command = container.Resolve<ICommand>(input);

            }

            return new Tuple<ICommand, object>(command, parameter);
        }
    }
}