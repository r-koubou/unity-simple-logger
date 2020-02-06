/* =========================================================================

    SimpleStreamLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// A typical ISimpleLogger implemetation with IO stream.
    /// </summary>
    public class SimpleStreamLogger : ISimpleLogger
    {
        private readonly object lockObject = new object();
        protected TextWriter writer;

        public string Name { get; set; }
        public ISimpleLogFormatter Formatter { get; set; }
        public bool OutputToOtherConsole { get; set; } = true;

        public virtual bool IsLogLevelAllowed( LogLevel logLevel )
        {
            return true;
        }

        public SimpleStreamLogger( string loggerName, Stream source, ISimpleLogFormatter formatter, bool autoFlush = false )
        {
            Name = loggerName;
            writer = new StreamWriter( source ) { AutoFlush = autoFlush };
            Formatter = formatter;
        }

        public SimpleStreamLogger( string loggerName, Stream source, bool autoFlush = false )
            : this( loggerName, source, SimpleLogFormatter.Default, autoFlush )
        {
        }

        public SimpleStreamLogger( Stream source, bool autoFlush = false )
            : this( "SimpleStreamLogger", source, SimpleLogFormatter.Default, autoFlush )
        {
        }

        virtual public void Dispose()
        {
            lock( lockObject )
            {
                writer?.Flush();
                writer?.Close();
                writer = null;
            }
        }

        public virtual void Log( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            lock( lockObject )
            {
                LogUnityConsole( level, message );
                writer.WriteLine( Formatter, level, message.ToString(), callerFilePath, callerLineNumber, callerMemberName );
            }
        }

        public virtual void LogException( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            lock( lockObject )
            {
                LogExceptionUnityConsole( exception );
                writer.WriteLine( Formatter, level, exception, callerFilePath, callerLineNumber, callerMemberName );
            }
        }
        public virtual Task LogAsync( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogUnityConsole( level, message );
            return writer.WriteLineAsync( Formatter, level, message, callerFilePath, callerLineNumber, callerMemberName );
        }

        public Task LogExceptionAsync( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogExceptionUnityConsole( exception );
            return writer.WriteExceptionLineAsync( Formatter, level, exception, callerFilePath, callerLineNumber, callerMemberName );
        }

        [Conditional( "UNITY_EDITOR" )]
        private void LogUnityConsole( LogLevel level, object message )
        {
            if( OutputToOtherConsole )
            {
                UnityEngine.Debug.unityLogger.Log( LogLevelConverter.ToUnity( level ), message );
            }
        }

        [Conditional( "UNITY_EDITOR" )]
        private void LogExceptionUnityConsole( Exception exception )
        {
            if( OutputToOtherConsole )
            {
                UnityEngine.Debug.unityLogger.LogException( exception );
            }
        }

    }
}
