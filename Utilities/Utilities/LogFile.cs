using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogFile:ILogger
    {
        private string LogFileName { get; set; }
        private string LogFilePath { get; set; }
        private string LogFileBaseName { get; set; }

        private int MAX_SIZE = 5000000;
        private bool StopLoop = false;
        private Task QueueTask = null;
        private Task CheckTask = null;
        private object lockObject = new object();
        public LogFile(string fileName)
        {
            LogFileName = fileName;
            LogFilePath = Path.GetDirectoryName(fileName);
            LogFileBaseName = Path.GetFileNameWithoutExtension(fileName);
        }

        public void Init()
        {
            QueueTask = Task.Run(() =>
            {
                while (!StopLoop)
                {
                    lock (Logger.logItemsQueue)
                    {
                        LogItem logItem = null;

                        if (Logger.logItemsQueue.Count > 0)
                        {
                            logItem = Logger.logItemsQueue.Dequeue();
                            if (logItem != null)
                            {
                                WriteLogToFile(logItem);
                            }                              
                        }
                    }

                    System.Threading.Thread.Sleep(1000);
                }
            });

            CheckTask = Task.Run(() =>
            {
                while (!StopLoop)
                {
                    LogCheckHoseKeeping();
                    // Sleep for half hour between checking the file size
                    System.Threading.Thread.Sleep(1000 * 60 * 60);
                }
            });
        }

        public void LogEvent(string msg)
        {
            try
            {
                LogItem logItem = new LogItem();
                logItem.LogType = "Event";
                logItem.DateTime = DateTime.Now;
                logItem.LogMessage = msg;
                Logger.logItemsQueue.Enqueue(logItem);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("An argument was null while logging an event", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected exception occurred while logging an event", ex);
            }
        }

        public void LogError(string msg)
        {
            try
            {
                LogItem logItem = new LogItem();
                logItem.LogType = "Error";
                logItem.DateTime = DateTime.Now;
                logItem.LogMessage = msg;
                Logger.logItemsQueue.Enqueue(logItem);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("An argument was null while logging an error", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected exception occurred while logging an error", ex);
            }
        }

        public void LogException(string msg, Exception exce)
        {
            try
            {
                LogItem logItem = new LogItem();
                logItem.LogType = "Exception";
                logItem.DateTime = DateTime.Now;
                logItem.Exception = exce;
                logItem.LogMessage = msg;
                Logger.logItemsQueue.Enqueue(logItem);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("An argument was null while logging an exception", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected exception occurred while logging an exception", ex);
            }
        }

        public void WriteLogToFile(LogItem logItem)
        {
            try
            {
                lock (lockObject)
                {
                    if (logItem.LogType == "Exception")
                    {
                        using (StreamWriter sw = new StreamWriter(LogFileName, true))
                        {
                            sw.WriteLine($"{logItem.DateTime}: {logItem.LogType}:  {logItem.Exception}-{logItem.LogMessage}");
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(LogFileName, true))
                        {
                            sw.WriteLine($"{logItem.DateTime}: {logItem.LogType}: {logItem.LogMessage}");
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access to the file was denied while writing log to file", ex);
            }
            catch (IOException ex)
            {
                Console.WriteLine("An I/O error occurred while writing log to file", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred while writing log to file", ex);
            }
        }

        private void CreateFile()
        {
            int countFileNumber = 1;

            while (System.IO.File.Exists(LogFileName))
            {
                LogFileName = $@"{LogFilePath}\{LogFileBaseName}{countFileNumber}.txt";

                countFileNumber++;
            }

            try
            {
                using (FileStream file = new FileStream(LogFileName, FileMode.Create)) ;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access to the file was denied while creating a new log file", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating a new log file", ex);
            }
        }

        public void LogCheckHoseKeeping()
        {
            if (!System.IO.File.Exists(LogFileName))
            {
                using (FileStream file = new FileStream(LogFileName, FileMode.Create)) ;
            }
            else
            {
                FileInfo fileInfo = new FileInfo(LogFileName);
                if (fileInfo.Length >= MAX_SIZE)
                {
                    CreateFile();
                }
            }
        }
    }
}
