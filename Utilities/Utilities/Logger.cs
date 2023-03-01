using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Logger
    {
        static ILogger logger;
        public static Queue<LogItem> logItemsQueue;

        public enum LogType
        {
            DB,
            File,
            Console,
            None
        }

        public Logger(LogType providerName)
        {
            logItemsQueue = new Queue<LogItem>();
            Init(providerName);
        }

        public void Init(LogType providerName)
        {
            switch (providerName)
            {
                case LogType.File:
                    logger = new LogFile(Environment.GetEnvironmentVariable("LogFileName"));

                    break;
                case LogType.DB:
                    logger = new LogDB();

                    break;
                case LogType.Console:
                    logger = new LogConsole();

                    break;

                default:
                    logger = new LogNone();

                    break;
            }
           
            logger.Init();      
        }

        public void LogEvent(string msg)
        {
            if (logger != null)
            {
                logger.LogEvent(msg);
            }
        }

        public void LogError(string msg)
        {
            if (logger != null)
            {
                logger.LogError(msg);
            }
        }

        public void LogException(string msg, Exception exce)
        {
            if (logger != null)
            {
                logger.LogException(msg, exce);
            }
        }
    }
}
