using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RKoubou.SimpleLogging;

public class LoggingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log( $"{nameof( LoggingObject )} : Start" );
        Debug.Log( "Logging via Debug.Log()" );
    }
}
