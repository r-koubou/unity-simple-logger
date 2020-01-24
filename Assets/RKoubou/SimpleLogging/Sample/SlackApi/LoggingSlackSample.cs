/* =========================================================================

    LoggingSlackSample.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.Net;
using System.Net.Mail;
using UnityEngine;
using RKoubou.SimpleLogging;

public class LoggingSlackSample : MonoBehaviour
{
    SimpleSlackLogger logger;

    [SerializeField]
    string token = "<Your Slack App Token>";

    [SerializeField]
    string channel = "<Channel Name>";

    [SerializeField]
    string prefix = "log";

    [SerializeField]
    string fileType = "log";

    const string DateFormat = "yyyy_MMdd_HHmmss";

    // Start is called before the first frame update
    void Start()
    {
        logger = new SimpleSlackLogger( new SimpleLogFormatter() );
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

    public void OnClickSendButton()
    {
        // Send log via Email
        var fileName = $"{prefix}_{System.DateTime.Now.ToString( DateFormat )}.{fileType}";
        SimpleSlackLogger.UploadParameter param = new SimpleSlackLogger.UploadParameter()
        {
            channel = channel,
            title = fileName,
            fileName = fileName,
            fileType = fileType
        };
        logger.SendToSlackAsync( token, param );
    }

    private void OnDestroy()
    {
        logger.LogDebug( "OnDestroy" );

        // logger have to release a resource when unused.
        logger.Dispose();
    }
}
