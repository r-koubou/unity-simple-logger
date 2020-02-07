using System.IO;
using RKoubou.SimpleLogging;
using UnityEngine;

#pragma warning disable 0649

public class UnityLogWrapper : MonoBehaviour
{
    private const string DateFormat = "yyyy_MMdd_HHmm_ss";

    private SimpleStreamLogger logger;

    [SerializeField]
    private LogLevel logLevel = LogLevel.Trace;

    private void Awake()
    {
#if UNITY_STANDALONE_WIN
        string logfilePath = Path.Combine( ".", $"{System.DateTime.Now.ToString( DateFormat )}.txt" );
#else
        string logfilePath = Path.Combine( Application.consoleLogPath, $"{System.DateTime.Now.ToString( DateFormat )}.txt" );
#endif
        logger = new SimpleStreamLogger(
            File.Create( logfilePath )
        );

        Application.logMessageReceivedThreaded += OnUnityLogMessageReceivedThreaded;

        // logMessageReceived 経由での出力しかしないので
        // logger内部で UnityEditor へのコンソールには出力させない
        logger.OutputToOtherConsole = false;

        logger.Level = logLevel;
    }

    private void OnDestroy()
    {
        logger?.Dispose();
        logger = null;
    }

    // Event that is fired if a log message is received.

    private void OnUnityLogMessageReceivedThreaded( string message, string stackTrace, LogType type )
    {
        LogLevel level = LogLevelConverter.FromUnity( type );
        logger?.Log( level, message );

        // logger?.LogRaw( level, formattedStackTrace );
        // or 行単位に分けたログ
        foreach( var line in stackTrace.Split( '\n' ) )
        {
            logger?.Log( level, $"    {line}" );
        }
    }

}
