using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;

namespace DynamicLog4net
{
    public sealed class DynamicLog
    {
        private static int _appenderNameCount = 0;
        private static int _logNameCount = 0;
        private readonly DynamicLogger _logger;
        private readonly string _conversionPattern;

        public DynamicLog()
        {
            _conversionPattern = "%m%newline%newline";
            _logger = new DynamicLogger(_logNameCount++.ToString());
        }

        public DynamicLog(string conversionPattern)
        {
            _conversionPattern = conversionPattern;
            _logger = new DynamicLogger(_logNameCount++.ToString());
        }

        static DynamicLog()
        {
            // Needed in order to create new logs. 
            BasicConfigurator.Configure();
        }

        public ILog GetLog()
        {
            return GetLog(Level.All);
        }

        public ILog GetLog(Level level)
        {
            _logger.Level = Level.All;
            return new LogImpl(_logger);
        }

        public void AddRollingFileAppender(string fileName, int maxDays)
        {
            var appender = new RollingFileAppender();
            appender.File = fileName;
            appender.MaxSizeRollBackups = maxDays;
            appender.RollingStyle = RollingFileAppender.RollingMode.Date;
            appender.DatePattern = ".yyyyMMdd"; // Roll the file every day.
            appender.AppendToFile = true;
            appender.StaticLogFileName = true;

            addAppender(appender);
        }

        private void addAppender(AppenderSkeleton appender)
        {
            addAppender(appender, getBasicLayout());
        }

        private void addAppender(AppenderSkeleton appender, LayoutSkeleton layout)
        {
            appender.Name = "SimpleAppender" + _appenderNameCount++.ToString();
            layout.ActivateOptions();
            appender.Layout = layout;
            appender.ActivateOptions();

            _logger.AddAppender(appender);
        }

        private LayoutSkeleton getBasicLayout()
        {
            var layout = new PatternLayout();
            layout.ConversionPattern = _conversionPattern;
            return layout;
        }
    }
}