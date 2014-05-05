# Dynamic Log4net

Log4net extension library for configuring appenders and loggers through c# code.

Basic Usage

    var dynamicLog = new DynamicLog("%m%newline");
    dynamicLog.AddRollingFileAppender(@"C:\log\transaction.log", 30);
    ILog logger = dynamicLog.GetLog();
