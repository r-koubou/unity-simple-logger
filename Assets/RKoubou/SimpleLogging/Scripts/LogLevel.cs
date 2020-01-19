/* =========================================================================

    LogLevel.cs
    Copyright(c) R-Koubou

   ======================================================================== */

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// Defenition of log level.
    /// See also <a href="https://en.wikipedia.org/wiki/Log4j#Log4j_log_levels">Log4j log levels</a>
    /// </summary>
    public enum LogLevel
    {
        Off,
        Trace,
        Assert,
        Debug,
        Info,
        Warning,
        Error,
        Fatal,
    }

    /// <summary>
    /// Converting value with <see cref="UnityEngine.LogType"/> and <see cref="LogLevel"/> utility.
    /// </summary>
    public static class LogLevelConverter
    {
        public static LogLevel FromUnity( UnityEngine.LogType logType )
        {
            switch( logType )
            {
                case UnityEngine.LogType.Log:
                    return LogLevel.Debug;

                case UnityEngine.LogType.Warning:
                    return LogLevel.Warning;

                case UnityEngine.LogType.Error:
                case UnityEngine.LogType.Exception:
                    return LogLevel.Error;

                case UnityEngine.LogType.Assert:
                    return LogLevel.Assert;

                default:
                    return LogLevel.Debug;
            }
        }

        public static UnityEngine.LogType ToUnity( LogLevel logLevel )
        {
            switch( logLevel )
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Info:
                    return UnityEngine.LogType.Log;

                case LogLevel.Warning:
                    return UnityEngine.LogType.Warning;

                case LogLevel.Error:
                case LogLevel.Fatal:
                    return UnityEngine.LogType.Error;

                case LogLevel.Assert:
                    return UnityEngine.LogType.Assert;

                default:
                    return UnityEngine.LogType.Log;
            }
        }
    }
}
