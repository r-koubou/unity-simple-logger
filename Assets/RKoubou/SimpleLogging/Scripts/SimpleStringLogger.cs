/* =========================================================================

    SimpleStringLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// A typical ISimpleLogger implemetation with StringBuilder
    /// </summary>
    abstract public class SimpleStringLogger : ISimpleLogger
    {

        protected readonly StringBuilder stringBuilder;
        protected StringWriter writer;

        public ISimpleLogFormatter Formatter { get; set; }

        public abstract bool IsLogLevelAllowed( LogLevel logLevel );

        public SimpleStringLogger( ISimpleLogFormatter formatter )
        {
            stringBuilder = new StringBuilder();
            writer = new StringWriter( stringBuilder );
            Formatter = formatter;
        }

        /// <summary>
        /// Clear messages from logged buffer on memory.
        /// </summary>
        public virtual void Clear()
        {
            stringBuilder.Clear();
        }

        public virtual void Dispose()
        {
            Clear();
            writer?.Flush();
            writer?.Close();
            writer = null;
        }

        public virtual void Log( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogUnityConsole( level, message );
            writer.WriteLine( Formatter, level, message.ToString(), callerFilePath, callerLineNumber, callerMemberName );
        }

        public virtual void LogException( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogExceptionUnityConsole( exception );
            writer.WriteLine( Formatter.ExceprionFormat( level, exception, callerFilePath, callerLineNumber, callerMemberName ) );
        }

        public virtual Task LogAsync( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogUnityConsole( level, message );
            return writer.WriteLineAsync( Formatter, level, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        public virtual Task LogExceptionAsync( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogExceptionUnityConsole( exception );
            return writer.WriteExceptionLineAsync( Formatter, level, exception, callerFilePath, callerLineNumber, callerMemberName );
        }

        [Conditional( "UNITY_EDITOR" )]
        protected virtual void LogUnityConsole( LogLevel level, object message )
        {
            UnityEngine.Debug.unityLogger.Log( LogLevelConverter.ToUnity( level ), message );
        }

        [Conditional( "UNITY_EDITOR" )]
        protected virtual void LogExceptionUnityConsole( Exception exception )
        {
            UnityEngine.Debug.unityLogger.LogException( exception );
        }
    }
}
