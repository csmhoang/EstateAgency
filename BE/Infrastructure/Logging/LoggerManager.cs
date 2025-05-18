﻿using NLog;

namespace Infrastructure;

public class LoggerManager : ILoggerManager
{
    #region Declaration
    private static ILogger logger = LogManager.GetCurrentClassLogger();
    #endregion

    #region Property
    #endregion

    #region Constructor
    public LoggerManager() { }
    #endregion

    #region Method
    public void LogDebug(string message) => logger.Debug(message);

    public void LogError(string message) => logger.Error(message);

    public void LogInfo(string message) => logger.Info(message);

    public void LogWarn(string message) => logger.Warn(message);
    #endregion

}
