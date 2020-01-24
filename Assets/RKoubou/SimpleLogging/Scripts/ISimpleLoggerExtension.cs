/* =========================================================================

    ISimpleLoggerExtension.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// Extension for ISimpleLogger
    /// </summary>
    public static class ISimpleLoggerExtension
    {

#region Synchronous methods

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
            logger.LogException( LogLevel.Error, exception, callerFilePath, callerLineNumber, callerMemberName );
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

        #endregion

#region Asynchronous methods

        /// <summary>
        /// Log with <see cref="LogLevel.Debug"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static Task LogDebugAsync(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            return logger.LogAsync( LogLevel.Debug, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Warning"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static Task LogWaringAsync(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            return logger.LogAsync( LogLevel.Warning, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Error"/> alias of <see cref="ISimpleLogger.LogException(LogLevel, System.Exception, string, int, string)"/>
        /// </summary>
        public static Task LogExceptionAsync(
            this ISimpleLogger logger,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            return logger.LogExceptionAsync( LogLevel.Error, exception, callerFilePath, callerLineNumber, callerMemberName );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Error"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static Task LogErrorAsync(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            return logger.LogAsync( LogLevel.Error, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        /// <summary>
        /// Log with <see cref="LogLevel.Fatal"/> alias of <see cref="ISimpleLogger.Log(LogLevel, object, string, int, string)"/>
        /// </summary>
        public static Task LogFatalAsync(
            this ISimpleLogger logger,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            return logger.LogAsync( LogLevel.Fatal, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        #endregion

    }
}
