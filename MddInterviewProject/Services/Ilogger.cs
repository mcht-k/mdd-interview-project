using System;

namespace MddInterviewProject.Services
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception ex);
    }
}