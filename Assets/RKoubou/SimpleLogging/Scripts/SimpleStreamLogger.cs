/* =========================================================================

    SimpleStreamLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// A typical ISimpleLogger implemetation with IO stream.
    /// </summary>
    public class SimpleStreamLogger : ISimpleLogger
    {
        private StreamWriter writer;
        public ISimpleLogFormatter Formatter { get; set; }

        public SimpleStreamLogger(
            string outputPath,
            ISimpleLogFormatter formatter,
            FileMode fileMode = FileMode.Append,
            bool autoFlush = false )
            : this( new FileStream( outputPath, fileMode ), formatter, autoFlush )
        {
        }

        public bool IsLogLevelAllowed( LogLevel logLevel )
        {
            return true;
        }

        public SimpleStreamLogger( Stream source, ISimpleLogFormatter formatter, bool autoFlush = false )
        {
            writer = new StreamWriter( source ) { AutoFlush = autoFlush };
            Formatter = formatter;
        }

        public void Dispose()
        {
            writer?.Flush();
            writer?.Close();
            writer = null;
        }

        public void Log( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogUnityConsole( level, message );

            writer.WriteLine(
                Formatter.Format(
                    level,
                    message.ToString(),
                    callerFilePath,
                    callerLineNumber,
                    callerMemberName )
            );
        }

        public void LogException( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
        {
            LogExceptionUnityConsole( exception );

            writer.WriteLine(
                Formatter.ExceprionFormat(
                    level,
                    exception,
                    callerFilePath,
                    callerLineNumber,
                    callerMemberName )
            );
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
