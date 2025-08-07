using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebugManager
{
    static DebugManager _instance;
    public static DebugManager Instance { get => _instance; }
    private bool DEBUGMODE;

    public DebugManager(bool ActiveDebug)
    {
        if (_instance != null)
            return;
        _instance = this;
    #if UNITY_EDITOR
        if(ActiveDebug)
            DEBUGMODE = true;
        else
            DEBUGMODE = false;
    #else
        DEBUGMODE = false;
    #endif
    }

    public static bool isDebug()
    {
        return _instance.DEBUGMODE;
    }

}
