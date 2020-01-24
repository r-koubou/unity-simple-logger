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

        public SimpleEmailLogger( ISimpleLogFormatter formatter ) : base( formatter )
        {
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
    }
}
