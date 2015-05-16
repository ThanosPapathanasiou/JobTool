using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace JobTool.Interceptors {
    public class LoggingInterceptor :IInterceptor {
        private readonly ILogger logger = NullLogger.Instance;

        public LoggingInterceptor() {

        }

        public LoggingInterceptor(ILogger logger) {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation) {
            logger.Debug(string.Format("Tracing: BeforeInsert: Class: {0} Method: {1}", invocation.Proxy, invocation.Method));
            invocation.Proceed();
            logger.Debug(string.Format("Tracing:  AfterInsert: Class: {0} Method: {1}", invocation.Proxy, invocation.Method));
        }

    }
}
