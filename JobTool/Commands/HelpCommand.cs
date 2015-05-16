using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;
using JobTool.Base;
using JobTool.Base.Interfaces;
using JobTool.Base.Interfaces.Services;

namespace JobTool.Commands
{
    public class HelpCommand : ICommand, IHelp
    {
        private readonly IKernel container;
        private readonly IUserInterfaceService ui;

        public HelpCommand(IKernel container, IUserInterfaceService ui)
        {
            this.container = container;
            this.ui = ui;
        }

        public void Help()
        {
            ui.Output("Inception!");
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                ui.Output("The available commands are:");
                foreach (ComponentModel item in container.GraphNodes)
                {
                    if (item.Implementation.GetInterfaces().Contains(typeof (ICommand)))
                        ui.Output(item.ComponentName.Name);
                }
            }
            else if (parameter is IHelp)
            {
                ((IHelp) parameter).Help();
            }
            else if (parameter is ICommand)
            {
                ui.Output(String.Format("Command '{0}' doesn't provide help functionality", parameter));
            }
        }




        public bool CanExecute(object parameter)
        {
            if ((parameter == null) || (parameter is ICommand))
            {
                return true;
            }
            return false;
        }

        public event EventHandler CanExecuteChanged;


    }
}
