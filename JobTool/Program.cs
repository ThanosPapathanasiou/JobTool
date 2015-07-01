using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core.Logging;
using Castle.Windsor;
using JobTool.Base;
using JobTool.Base.Interfaces;
using JobTool.Base.Interfaces.Services;

namespace JobTool
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var container = Infrastructure.BootstrapContainer()
                                          .ApplyPreferences(useNLog:true,
                                                            useLoggingInterceptor:true);
            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}