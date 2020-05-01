using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace _2ndbrainalpha
{
    public class Logger : ILogger
    {
        private readonly string _logFilePath;
        private ReaderWriterLock _lock = new ReaderWriterLock();
        private int _lockTimeoutMs = 5000;
        public Logger(string path)
        {
            _logFilePath = $@"{path}\log.txt";
        }

        public void Log(string msg)
        {
            Log(new[] { msg });
        }

        public void LogException(Exception ex)
        {
            Log(new[] { ex.Message, ex.StackTrace });
        }

        private void Log(string[] msgs)
        {
            try
            {
                _lock.AcquireWriterLock(_lockTimeoutMs);
                File.AppendAllLines(_logFilePath, msgs.Select(TimeStamp));
            }
            finally
            {
                _lock.ReleaseWriterLock();
            }
        }

        private string TimeStamp(string msg)
        {
            return $"[{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff}] {msg}";
        }
    }
}
