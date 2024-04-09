
using log4net;
using System;
namespace MddInterviewProject.Services
{

    public class Logger : ILogger
    {
        private readonly ILog _log;

        public Logger(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }
    }
}