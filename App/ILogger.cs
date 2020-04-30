using System;

namespace _2ndbrainalpha
{
    public interface ILogger
    {
        void Log(string msg);
        void LogException(Exception ex);
    }
}