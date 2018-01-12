using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace NSI.Logger
{
    public static class Logger
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));
        private static ILoggerRepository logRepository;

        private static void InitLogger()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            logRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(logRepository, log4netConfig["log4net"]);
        }

        public static void LogError(string message)
        {
            if (logRepository == null)
            {
                InitLogger();
            }
            log.Error(message);
        }

        public static void LogError(Exception exception)
        {
            if (logRepository == null)
            {
                InitLogger();
            }
            log.Error(exception);
        }

        public static void LogInfo(string message)
        {
            if (logRepository == null)
            {
                InitLogger();
            }
            log.Info(message);
        }
    }
}
