using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;

namespace JobTool.HandlerSelectors
{
    public class GenericHandlerSelector : IHandlerSelector
    {
        private readonly Func<Type, bool> filter;
        private readonly Func<IHandler, bool> predicate;

        public GenericHandlerSelector(Func<Type, bool> filter, Func<IHandler, bool> predicate)
        {
            this.filter = filter;
            this.predicate = predicate;
        }

        public bool HasOpinionAbout(string key, Type service)
        {
            return filter(service);
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            return handlers.First(predicate);
        }
    }
}
