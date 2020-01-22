/* =========================================================================

    SimpleEmailLogger.cs
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
    /// A typical ISimpleLogger implemetation with Email
    /// </summary>
    public class SimpleEmailLogger : ISimpleLogger
    {

        public struct SendMailInfo
        {
            public string sendFrom;
            public string sendTo;
            public string subject;
            public string host;
            public int port;
            public SmtpDeliveryMethod method;
            public NetworkCredential networkCredential;
        }

        protected readonly StringBuilder stringBuilder;
        protected StringWriter writer;

        public ISimpleLogFormatter Formatter { get; set; }

        public virtual bool IsLogLevelAllowed( LogLevel logLevel )
        {
            return true;
        }

        public SimpleEmailLogger( ISimpleLogFormatter formatter )
        {
            stringBuilder = new StringBuilder();
            writer = new StringWriter( stringBuilder );
            Formatter = formatter;
        }

        public void Clear()
        {
            stringBuilder.Clear();
        }

        virtual public void Dispose()
        {
            Clear();
            writer?.Flush();
            writer?.Close();
            writer = null;
        }

        public virtual void Log( LogLevel level, object message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
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

        public virtual void LogException( LogLevel level, Exception exception, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMemberName = "" )
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

        public void SendEmail( SendMailInfo info )
        {
            lock( stringBuilder )
            {
                using( MailMessage mailMessage = new MailMessage() )
                using( SmtpClient client = new SmtpClient() )
                {
                    mailMessage.From = new MailAddress( info.sendFrom );
                    mailMessage.To.Add( info.sendTo );
                    mailMessage.Subject = info.subject;
                    mailMessage.Body = stringBuilder.ToString();

                    client.Host = info.host;
                    client.Port = info.port;
                    client.EnableSsl = true;
                    client.DeliveryMethod = info.method;

                    if( info.networkCredential != null )
                    {
                        client.Credentials = info.networkCredential;
                    }

                    client.Send( mailMessage );
                }
            }
        }

        public void SendEmailAsync( SendMailInfo info )
        {
            Task.Run( () => {
                SendEmail( info );
            });
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
