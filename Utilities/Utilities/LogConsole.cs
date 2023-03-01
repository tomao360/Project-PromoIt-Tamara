using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogConsole:ILogger
    {
        private bool StopLoop = false;
        private Task QueueTask = null;

        public void Init()
        {
            QueueTask = Task.Run(() =>
            {
                while (!StopLoop)
                {
                    LogItem logItem = null;

                    lock (Logger.logItemsQueue)
                    {
                        if (Logger.logItemsQueue.Count > 0)
                        {
                            logItem = Logger.logItemsQueue.Dequeue();
                            if (logItem != null)
                            {
                                WriteToConsole(logItem);
                            }
                        }
                    }

                    System.Threading.Thread.Sleep(1000);
                }
            });
        }

        public void LogEvent(string msg)
        {
            LogItem logItem = new LogItem();
            logItem.LogType = "Event";
            logItem.DateTime = DateTime.Now;
            logItem.LogMessage = msg;
            Logger.logItemsQueue.Enqueue(logItem);
        }

        public void LogError(string msg)
        {
            LogItem logItem = new LogItem();
            logItem.LogType = "Error";
            logItem.DateTime = DateTime.Now;
            logItem.LogMessage = msg;
            Logger.logItemsQueue.Enqueue(logItem);
        }

        public void LogException(string msg, Exception exce)
        {
            LogItem logItem = new LogItem();
            logItem.LogType = "Exception";
            logItem.DateTime = DateTime.Now;
            logItem.Exception = exce;
            logItem.LogMessage = msg;
            Logger.logItemsQueue.Enqueue(logItem);
        }

        public void WriteToConsole(LogItem logItem)
        {
            if (logItem.LogType == "Exception")
            {
                Console.WriteLine($"{logItem.DateTime}: {logItem.LogType}:  {logItem.Exception}-{logItem.LogMessage}");
            }
            else
            {
                Console.WriteLine($"{logItem.DateTime}: {logItem.LogType}: {logItem.LogMessage}");
            }
           
        }
        public void LogCheckHoseKeeping()
        {

        }
    }
}
