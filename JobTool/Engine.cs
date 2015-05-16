using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core.Logging;
using Castle.MicroKernel;
using JobTool.Base;
using JobTool.Base.Interfaces.Services;
using JobTool.Base.Services;

namespace JobTool {
    public class Engine: IEngine {

        private readonly ILogger logger;
        private readonly IUserInterfaceService ui;
        private readonly ICommandParserService commandParser;

        public Engine(IUserInterfaceService ui, ILogger logger, ICommandParserService commandParser)
        {
            this.ui = ui;
            this.logger = logger;
            this.commandParser = commandParser;
        }


        public void Run() {

            ui.Output("Type 'help' to show available commands and 'quit' to quit");

            string input = "help";
            while(input != "quit")
            {

                var results = commandParser.Parse(input);

                ICommand command = results.Item1;
                object parameter = results.Item2;

                if (command.CanExecute(parameter))
                {
                    try
                    {
                        command.Execute(parameter);
                    }
                    catch(Exception ex)
                    {
                        string errorMessage = String.Format("Error executing command '{0}' with parameter '{1}'",
                            command, parameter);
                        ui.Output(errorMessage);
                        logger.Error(errorMessage,ex);
                    }
                }
                else
                {
                    ui.Output(String.Format("Command '{0}' cannot execute with parameter '{1}'", command, parameter));
                }

                input = ui.Input();
            }

        }

    }
}
