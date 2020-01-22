/* =========================================================================

    LoggingSample.cs
    Copyright(c) R-Koubou

   ======================================================================== */

using System.IO;
using UnityEngine;
using RKoubou.SimpleLogging;

public class LoggingSample : MonoBehaviour
{
    ISimpleLogger logger;

    [SerializeField]
    string logFilePath = "logdemo.txt";

    // Start is called before the first frame update
    void Start()
    {
        logger = new SimpleStreamLogger( new FileStream( logFilePath, FileMode.Append ), new SimpleLogFormatter() );
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

    private void OnDestroy()
    {
        logger.LogDebug( "OnDestroy" );

        // logger have to release a resource when unused.
        logger.Dispose();
    }


}
