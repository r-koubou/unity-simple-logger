/* =========================================================================

    ISimpleLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
        /// Check logging is enabled from given log level.
        /// </summary>
        bool IsLogLevelAllowed( LogLevel logLevel );

#region Synchronous methods

        /// <summary>
        /// Log a message with formatter.
        /// </summary>
        void Log(
            LogLevel level,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );

        /// <summary>
        /// Log a message with formatter.
        /// </summary>
        void LogException(
            LogLevel level,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );
        #endregion

#region Asynchronous methods

        /// <summary>
        /// Log a message with formatter.
        /// </summary>
        Task LogAsync(
            LogLevel level,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );

        /// <summary>
        /// Log a message with formatter.
        /// </summary>
        Task LogExceptionAsync(
            LogLevel level,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );

#endregion

    }


}
