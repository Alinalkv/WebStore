using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetLoggerExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string CongigurationFile = "log4net.config")
        {
           if(!Path.IsPathRooted(CongigurationFile))
            {
                var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Не определена сборка с точкой входа");
                var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Не определена директория");
                CongigurationFile = Path.Combine(dir, CongigurationFile);
            }

            Factory.AddProvider(new Log4NetLoggerProvider(CongigurationFile));
            return Factory;
        }
    }
}
