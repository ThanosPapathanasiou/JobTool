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
using JobTool.Base.Services;
using JobTool.HandlerSelectors;
using JobTool.Interceptors;
using JobTool.Services;


namespace JobTool
{
    public static class Infrastructure
    {

        public static IWindsorContainer BootstrapContainer()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Register(Component.For<IEngine>().ImplementedBy<Engine>().LifestyleSingleton());
            container.RegisterCommands();
            container.RegisterRepositories();
            container.RegisterServices();

            return container;
        }

        public static IWindsorContainer ApplyPreferences(
            this IWindsorContainer container,
            bool useNLog = true,
            bool useLoggingInterceptor = false,
            bool useMemoizationFibonacciService = true)
        {
            if (useNLog)
                container.AddFacility<LoggingFacility>(
                    f => f.LogUsing(LoggerImplementation.NLog).WithConfig("NLog.config"));

            if (useLoggingInterceptor)
                container.Register(Component.For<LoggingInterceptor>().LifestyleTransient());

            var memoizationHandler =
                new GenericHandlerSelector(
                    (filter) => filter == typeof (IFibonacciCalculatorService),
                    (handler) => useMemoizationFibonacciService
                        ? handler.ComponentModel.Implementation == typeof (MemoizedCalculateFibonacciService)
                        : handler.ComponentModel.Implementation == typeof (FibonacciCalculatorService)
                    );

            container.Kernel.AddHandlerSelector(memoizationHandler);


            return container;
        }

        private static void RegisterCommands(this IWindsorContainer container)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<ICommand>()
                    .LifestyleTransient()
                    .Configure(
                        component =>
                            component.Named(component.Implementation.Name.TrimEnd("Command").ToLowerInvariant()))
                );
        }

        private static void RegisterServices(this IWindsorContainer container)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IService>()
                    .WithService
                    .FromInterface()
                    .LifestyleSingleton()
                );
        }

        private static void RegisterRepositories(this IWindsorContainer container)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IRepository>()
                    .WithService
                    .FromInterface()
                    .LifestyleTransient()
                );
        }

        private static string TrimEnd(this string s, string t)
        {
            if (s.EndsWith(t))
            {
                return s.Substring(0, s.Length - t.Length);
            }
            return s;
        }
    }
}
