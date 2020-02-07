/* =========================================================================

    SimpleLogFormatter.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// A typical ISimpleLogFormatter implemetation.
    /// </summary>
    public class SimpleLogFormatter : ISimpleLogFormatter
    {
        private static readonly string DateFormatString = "yyyy/MM/dd";
        private static readonly string TimeFormatString = "HH:mm:ss.fff";
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public static readonly SimpleLogFormatter Default = new SimpleLogFormatter();

        public bool LoggingFileNameOnly { get; set; } = false;

        public string ExceprionFormat(
            LogLevel logLevel,
            Exception exception,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            lock( stringBuilder )
            {
                stringBuilder.Clear();

                GenerateHeader( stringBuilder, logLevel, callerFilePath, callerLineNumber, callerMemberName )
                    .Append( " Exception" )
                    .Append( Environment.NewLine )
                    .Append( exception.StackTrace );

                return stringBuilder.ToString();
            }
        }

        public string Format(
            LogLevel logLevel,
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMemberName = "" )
        {
            lock( stringBuilder )
            {
                stringBuilder.Clear();

                GenerateHeader( stringBuilder, logLevel, callerFilePath, callerLineNumber, callerMemberName )
                    .Append( message );

                return stringBuilder.ToString();
            }
        }

        private StringBuilder GenerateHeader(
            StringBuilder builder,
            LogLevel logLevel,
            string callerFilePath,
            int callerLineNumber,
            string callerMemberName )
        {
            if( LoggingFileNameOnly )
            {
                callerFilePath = Path.GetFileName( callerFilePath );
            }

            DateTime now = DateTime.Now;

            builder.Append( "[" )
            .Append( logLevel )
            .Append( "]" )
            .Append( "\t" )
            .Append( now.ToString( DateFormatString ) )
            .Append( "\t" )
            .Append( now.ToString( TimeFormatString ) )
            .Append( "\t" )
            .Append( $"{callerFilePath}:{callerLineNumber}" )
            .Append( "\t" );
            return builder;
        }
    }
}
