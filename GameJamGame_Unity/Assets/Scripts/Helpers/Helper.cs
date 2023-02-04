using System.Collections;
using UnityEngine;

public enum DebugType
{
    Log,
    Warning,
    Error
}

public static class Helper
{

    public static void InternalDebugLog(object obj, DebugType type = DebugType.Log)
    {
#if UNITY_EDITOR
        switch (type)
        {
            case DebugType.Log:
                Debug.Log(obj);
                break;
            case DebugType.Warning:
                Debug.LogWarning(obj);
                break;
            case DebugType.Error:
                Debug.LogError(obj);
                break;
            default:
                break;
        }        
#endif
    }
}