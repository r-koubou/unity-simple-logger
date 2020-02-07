/* =========================================================================

    ISimpleLogFormatter.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Runtime.CompilerServices;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// Interface for formatting message.
    /// </summary>
    public interface ISimpleLogFormatter
    {
        /// <summary>
        /// If true, formatter will output a callerFilePath as filename only.
        /// </summary>
        bool LoggingFileNameOnly { get; set; }

        /// <summary>
        /// Create a formated message from given parameters.
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="message">message</param>
        /// <param name="callerFilePath">A CallerFilePath value</param>
        /// <param name="callerLineNumber">A CallerLineNumber value</param>
        /// <param name="callerMemberName">A CallerMemberName value</param>
        /// <returns>Formatted message</returns>
        string Format(
            LogLevel logLevel,
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );

        /// <summary>
        /// Create a formated message from given parameters when exception occurred.
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="exception">A Exception object</param>
        /// <param name="callerFilePath">A CallerFilePath value</param>
        /// <param name="callerLineNumber">A CallerLineNumber value</param>
        /// <param name="callerMemberName">A CallerMemberName value</param>
        /// <returns>Formatted message</returns>
        string ExceprionFormat(
            LogLevel logLevel,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" );
    }
}
