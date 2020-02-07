/* =========================================================================

    SimpleStringLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System;
using System.Diagnostics;
using System.IO;
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
        private readonly object lockObject = new object();
        protected readonly StringBuilder stringBuilder;
        protected StringWriter writer;

        public string Name { get; set; }
        public ISimpleLogFormatter Formatter { get; set; }

        public LogLevel Level { get; set; } = LogLevel.Trace;

        public bool OutputToOtherConsole { get; set; } = true;

        public SimpleStringLogger( string loggerName, ISimpleLogFormatter formatter )
        {
            Name = loggerName;
            stringBuilder = new StringBuilder();
            writer = new StringWriter( stringBuilder );
            Formatter = formatter;
        }

        public SimpleStringLogger( string loggerName = "SimpleStringLogger" ) : this( loggerName, SimpleLogFormatter.Default )
        {
        }

        public virtual bool IsLogLevelAllowed( LogLevel logLevel )
        {
            return logLevel >= Level;
        }

        /// <summary>
        /// Clear messages from logged buffer on memory.
        /// </summary>
        public virtual void Clear()
        {
            lock( lockObject )
            {
                stringBuilder.Clear();
            }
        }

        public virtual void Dispose()
        {
            lock( lockObject )
            {
                Clear();
                writer?.Flush();
                writer?.Close();
                writer = null;
            }
        }

        public virtual void LogRaw( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            if( !IsLogLevelAllowed( level ) )
            {
                return;
            }
            lock( lockObject )
            {
                LogUnityConsole( level, message );
                writer.WriteLine( message.ToString() );
            }
        }

        public virtual void Log( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            if( !IsLogLevelAllowed( level ) )
            {
                return;
            }
            lock( lockObject )
            {
                LogUnityConsole( level, message );
                writer.WriteLine( Formatter, level, message.ToString(), callerFilePath, callerLineNumber, callerMemberName );
            }
        }

        public virtual void LogException( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            if( !IsLogLevelAllowed( level ) )
            {
                return;
            }
            lock( lockObject )
            {
                LogExceptionUnityConsole( exception );
                writer.WriteLine( Formatter.ExceprionFormat( level, exception, callerFilePath, callerLineNumber, callerMemberName ) );
            }
        }

        public virtual Task LogAsync( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            if( !IsLogLevelAllowed( level ) )
            {
                return Task.CompletedTask;
            }
            LogUnityConsole( level, message );
            return writer.WriteLineAsync( Formatter, level, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        public virtual Task LogExceptionAsync( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            if( !IsLogLevelAllowed( level ) )
            {
                return Task.CompletedTask;
            }
            LogExceptionUnityConsole( exception );
            return writer.WriteExceptionLineAsync( Formatter, level, exception, callerFilePath, callerLineNumber, callerMemberName );
        }

        [Conditional( "UNITY_EDITOR" )]
        protected virtual void LogUnityConsole( LogLevel level, object message )
        {
            if( OutputToOtherConsole )
            {
                UnityEngine.Debug.unityLogger.Log( LogLevelConverter.ToUnity( level ), message );
            }
        }

        [Conditional( "UNITY_EDITOR" )]
        protected virtual void LogExceptionUnityConsole( Exception exception )
        {
            if( OutputToOtherConsole )
            {
                UnityEngine.Debug.unityLogger.LogException( exception );
            }
        }
    }
}
