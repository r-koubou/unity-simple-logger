/* =========================================================================

    TextWriterExtension.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// Extension for System.IO.TextWriter which gives WriteLine for logging.
    /// </summary>
    public static class TextWriterExtension
    {

#region Synchronous methods

        /// <summary>
        /// Writer will write a message with foemated text with formatter.
        /// </summary>
        public static void WriteLine(
            this TextWriter writer,
            ISimpleLogFormatter formatter,
            LogLevel logLevel,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            writer.WriteLine(
                formatter.Format(
                    logLevel,
                    message.ToString(),
                    callerFilePath,
                    callerLineNumber,
                    callerMemberName )
            );
        }

        /// <summary>
        /// Writer will write a message with foemated text with formatter.
        /// </summary>
        public static void WriteExceptionLine(
            this TextWriter writer,
            ISimpleLogFormatter formatter,
            LogLevel level,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            writer.WriteLine(
                formatter.ExceprionFormat(
                    level,
                    exception,
                    callerFilePath,
                    callerLineNumber,
                    callerMemberName )
            );
        }

#endregion

#region Asynchronous methods

        /// <summary>
        /// Writer will write a message with foemated text with formatter.
        /// </summary>
        public static Task WriteLineAsync(
            this TextWriter writer,
            ISimpleLogFormatter formatter,
            LogLevel logLevel,
            object message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            return Task.Run( () => {
                writer.WriteLineAsync(
                    formatter.Format(
                        logLevel,
                        message.ToString(),
                        callerFilePath,
                        callerLineNumber,
                        callerMemberName )
                );
            });
        }


        /// <summary>
        /// Writer will write a message with foemated text with formatter.
        /// </summary>
        public static Task WriteExceptionLineAsync(
            this TextWriter writer,
            ISimpleLogFormatter formatter,
            LogLevel level,
            System.Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            return Task.Run( () => {
                writer.WriteLineAsync(
                    formatter.ExceprionFormat(
                        level,
                        exception,
                        callerFilePath,
                        callerLineNumber,
                        callerMemberName )
                );
            });
        }

#endregion

    }
}
