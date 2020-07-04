using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _CongigurationFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> _Loggers = new ConcurrentDictionary<string, Log4NetLogger>();

       public Log4NetLoggerProvider(string CongigurationFile)
        {
            _CongigurationFile = CongigurationFile;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _Loggers.GetOrAdd(categoryName, category =>
            {
                var xml = new XmlDocument();
                xml.Load(_CongigurationFile);
                return new Log4NetLogger(category, xml["log4net"]);
            });
        }
        public void Dispose() => _Loggers.Clear();
    }

}
