/* =========================================================================

    ISimpleLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Runtime.CompilerServices;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// Interface for logger implementation.
    /// </summary>
    public interface ISimpleLogger : System.IDisposable
    {
        /// <summary>
        /// For creating a formatted message
        /// </summary>
        ISimpleLogFormatter Formatter { get; set; }

        /// <summary>
        /// Log a message with formatter.
        /// </summary>
        void LogException(
            LogLevel level,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );

        /// <summary>
        /// Log a message with formatter.
        /// </summary>
        void Log(
            LogLevel level,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );

    }

    /// <summary>
    /// Extension for ISimpleLogger
    /// </summary>
    public static class ISimpleLoggerExtension
    { 
        /// <summary>
        /// Log with <see cref="LogLevel.Debug"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static void LogDebug(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            logger.Log( LogLevel.Debug, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Warning"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static void LogWaring(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            logger.Log( LogLevel.Warning, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Error"/> alias of <see cref="ISimpleLogger.LogException(LogLevel, System.Exception, string, int, string)"/>
        /// </summary>
        public static void LogException(
            this ISimpleLogger logger,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            logger.LogException( LogLevel.Error, exception );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Error"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static void LogError(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            logger.Log( LogLevel.Error, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Fatal"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static void LogFatal(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            logger.Log( LogLevel.Fatal, message, callerFilePath, callerLineNumber, callerMemberName );
        }
    }
}
