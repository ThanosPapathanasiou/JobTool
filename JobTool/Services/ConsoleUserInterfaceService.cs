using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Castle.Core;
using JobTool.Base.Interfaces.Services;
using JobTool.Interceptors;

namespace JobTool.Services {
    public class ConsoleUserInterfaceService : IUserInterfaceService {
        public void Output(string message) {
            Console.WriteLine(message);
        }

        public string Input() {
            return Console.ReadLine();
        }
    }
}
