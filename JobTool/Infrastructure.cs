using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using JobTool.Base;
using JobTool.Base.Interfaces;
using JobTool.Interceptors;


namespace JobTool {
    public static class Infrastructure {

        public static IWindsorContainer BootstrapContainer() {
            IWindsorContainer container = new WindsorContainer();

            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.NLog).WithConfig("NLog.config"));

            container.Register(Component.For<IEngine>().ImplementedBy<Engine>().LifestyleSingleton());
            container.Register(Component.For<LoggingInterceptor>().LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                       .BasedOn<ICommand>()
                       .LifestyleTransient()
                       .Configure(component => component.Named(component.Implementation.Name.TrimEnd("Command").ToLowerInvariant()))
            );

            container.Register(
               Classes.FromThisAssembly()
                      .BasedOn<IService>()
                      .WithService
                      .FromInterface()
                      .LifestyleSingleton()
             );

            container.Register(
               Classes.FromThisAssembly()
                      .BasedOn<IRepository>()
                      .WithService
                      .FromInterface()
                      .LifestyleTransient()
             );
            return container;
        }

        private static string TrimEnd(this string s, string t){
            if(s.EndsWith(t)) {
                return s.Substring(0, s.Length - t.Length);
            }
            return s;
        }
    }
}
