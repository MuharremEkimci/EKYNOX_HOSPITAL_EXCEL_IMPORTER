using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKYNOX_HEI.CORE.Helpers
{
    public class AppLogger
    {
        private static readonly ILogger _logger;

        static AppLogger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.File(
                    "Logs/app-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30)
                .CreateLogger();
        }

        public static void Info(string message)
        {
            _logger.Information(message);
        }

        public static void Warning(string message)
        {
            _logger.Warning(message);
        }

        public static void Error(string message)
        {
            _logger.Error(message);
        }

        public static void Error(DateTime ProcessDate, string ProcessLocation, string Event, string Method, string ErrMess )
        {
            string message = $@"ProcessDate: {ProcessDate}, ProcessLocation: {ProcessLocation}, Event: {Event}, Method: {Method} - Hata Detayı: {ErrMess}";
            _logger.Error(message);
        }
    }
}
