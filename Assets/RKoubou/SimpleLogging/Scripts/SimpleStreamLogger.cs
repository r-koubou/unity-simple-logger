/* =========================================================================

    SimpleStreamLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System;
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

        public SimpleStreamLogger( string outputPath, ISimpleLogFormatter formatter, FileMode fileMode = FileMode.Append )
            : this( new FileStream( outputPath, fileMode ), formatter )
        {
        }

        public SimpleStreamLogger( Stream source, ISimpleLogFormatter formatter)
        {
            writer = new StreamWriter( source ) { AutoFlush = true };
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

#if UNITY_EDITOR
            UnityEngine.Debug.unityLogger.Log( LogLevelConverter.ToUnity( level ), message );
#endif

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

#if UNITY_EDITOR
            UnityEngine.Debug.unityLogger.LogException( exception );
#endif

            writer.WriteLine(
                Formatter.ExceprionFormat(
                    level,
                    exception,
                    callerFilePath,
                    callerLineNumber,
                    callerMemberName )
            );
        }
    }
}
