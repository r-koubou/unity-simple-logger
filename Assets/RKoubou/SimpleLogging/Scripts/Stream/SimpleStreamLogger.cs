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

        public ISimpleLogFormatter Formatter { get; set; }

        public virtual bool IsLogLevelAllowed( LogLevel logLevel )
        {
            return true;
        }

        public SimpleStreamLogger( Stream source, ISimpleLogFormatter formatter, bool autoFlush = false )
        {
            writer = new StreamWriter( source ) { AutoFlush = autoFlush };
            Formatter = formatter;
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
            UnityEngine.Debug.unityLogger.Log( LogLevelConverter.ToUnity( level ), message );
        }

        [Conditional( "UNITY_EDITOR" )]
        private void LogExceptionUnityConsole( Exception exception )
        {
            UnityEngine.Debug.unityLogger.LogException( exception );
        }

    }
}
