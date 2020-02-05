/* =========================================================================

    SimpleEmailLogger.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RKoubou.SimpleLogging
{
    /// <summary>
    /// A typical ISimpleLogger implemetation with Email
    /// </summary>
    public class SimpleEmailLogger : SimpleStringLogger
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

        public override bool IsLogLevelAllowed( LogLevel logLevel )
        {
            return true;
        }

        public SimpleEmailLogger( string loggerName, ISimpleLogFormatter formatter ) : base( loggerName, formatter )
        {
        }

        public SimpleEmailLogger( string loggerName = "SimpleEmailLogger" )
            : this( loggerName, SimpleLogFormatter.Default )
        {
        }

        public void SendEmail( SendMailInfo info )
        {
            using( var mailMessage = new MailMessage() )
            using( var client = new SmtpClient() )
            {
                mailMessage.From = new MailAddress( info.sendFrom );
                mailMessage.To.Add( info.sendTo );
                mailMessage.Subject = info.subject;

                lock( stringBuilder )
                {
                    mailMessage.Body = stringBuilder.ToString();
                }

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

        public void SendEmailAsync( SendMailInfo info )
        {
            Task.Run( () => {
                SendEmail( info );
            });
        }
    }
}
