/* =========================================================================

    LoggingEmailSample.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Net;
using System.Net.Mail;
using UnityEngine;
using RKoubou.SimpleLogging;

public class LoggingEmailSample : MonoBehaviour
{
    SimpleEmailLogger logger;

    [SerializeField]
    string fromAddress = "from@example.com";

    [SerializeField]
    string toAddress = "to@example.com";

    [SerializeField]
    string subject = "Send from LoggingEmailSample";

    [SerializeField]
    string host = "smtp.gmail.com";

    [SerializeField]
    int port = 587;

    [SerializeField]
    string userName = "username@example.com";

    [SerializeField]
    string password = "password or app password in Google account";

    // Start is called before the first frame update
    void Start()
    {
        logger = new SimpleEmailLogger( new SimpleLogFormatter() );
        logger.LogDebug( "Start" );
        logger.LogWaring( "Warning" );
        try
        {
            throw new System.Exception( "Test Exception" );
        }
        catch( System.Exception ex )
        {
            logger.LogException( ex );
        }
    }

    public void SendEmail()
    {
        // Send log via Email
        SimpleEmailLogger.SendMailInfo info = new SimpleEmailLogger.SendMailInfo()
        {
            sendFrom = fromAddress,
            sendTo = toAddress,
            subject = subject,
            host = host,
            port = port,
            method = SmtpDeliveryMethod.Network,
            networkCredential = new NetworkCredential( userName, password )
        };
        logger.SendEmailAsync( info );
    }

    private void OnDestroy()
    {
        logger.LogDebug( "OnDestroy" );

        // logger have to release a resource when unused.
        logger.Dispose();
    }
}
