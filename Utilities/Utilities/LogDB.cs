using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogDB:ILogger
    {
        private string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        private bool StopLoop = false;
        private Task QueueTask = null;
        private Task CheckTask = null;

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
                                InsertLogToDB(logItem);
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
                    // Sleep for hour between checking the DB rcords date
                    System.Threading.Thread.Sleep(1000 * 60 * 60);
                }
            });
        }

        public void LogEvent(string msg)
        {
            LogItem logItem = new LogItem();
            logItem.LogType = "Event";
            logItem.LogMessage = msg;
            logItem.DateTime = DateTime.Now;
            Logger.logItemsQueue.Enqueue(logItem);
        }

        public void LogError(string msg)
        {
            LogItem logItem = new LogItem();
            logItem.LogType = "Error";
            logItem.LogMessage = msg;
            logItem.DateTime = DateTime.Now;
            Logger.logItemsQueue.Enqueue(logItem);
        }

        public void LogException(string msg, Exception exce)
        {
            LogItem logItem = new LogItem();
            logItem.LogType = "Exception";
            logItem.LogMessage = msg;
            logItem.Exception = exce;
            logItem.DateTime = DateTime.Now;
            Logger.logItemsQueue.Enqueue(logItem);
        }

        public void LogCheckHoseKeeping()
        {
            string delete = "delete from Logger\r\nwhere LogDate < dateadd(month, -3, getdate())";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(delete, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InsertLogToDB(LogItem logItem)
        {
            string queryInsert;

            if (logItem.LogType == "Event")
            {
                queryInsert = "declare @loggerID int\r\ninsert into Logger values(@message, 'NULL', 'NULL', 'NULL', @dateTime)\r\nselect @loggerID = @@IDENTITY";
            }
            else if (logItem.LogType == "Error")
            {
                queryInsert = "declare @loggerID int\r\ninsert into Logger values('NULL', @message, 'NULL', 'NULL', @dateTime)\r\nselect @loggerID = @@IDENTITY";
            }
            else
            {
                queryInsert = "declare @loggerID int\r\ninsert into Logger values('NULL', 'NULL', @exception, @message, @dateTime)\r\nselect @loggerID = @@IDENTITY";
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(queryInsert, connection))
                    {
                        connection.Open();

                        if (logItem.LogType == "Event" || logItem.LogType == "Error")
                        {
                            command.Parameters.AddWithValue("@message", logItem.LogMessage);
                            command.Parameters.AddWithValue("@dateTime", logItem.DateTime);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@message", logItem.LogMessage);
                            command.Parameters.AddWithValue("@exception", logItem.Exception.ToString());
                            command.Parameters.AddWithValue("@dateTime", logItem.DateTime);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
