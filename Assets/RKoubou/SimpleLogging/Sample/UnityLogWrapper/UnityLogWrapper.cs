
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using RKoubou.SimpleLogging;

#pragma warning disable 0649

public class UnityLogWrapper : MonoBehaviour
{
    private const string LogDirectory = ".";
    private const string DateFormat = "yyyy_MMdd_HHmm_ss";

    private SimpleStreamLogger logger;

    private void Awake()
    {
        logger = new SimpleStreamLogger(
            File.Create( $"{System.DateTime.Now.ToString(DateFormat)}.txt" )
        );

        Application.logMessageReceivedThreaded += OnUnityLogMessageReceivedThreaded;

        // logMessageReceived 経由での出力しかしないので
        // logger内部で UnityEditor へのコンソールには出力させない
        logger.OutputToOtherConsole = false;
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
        logger?.LogRaw( level, stackTrace );
    }

}
