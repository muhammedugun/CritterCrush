using UnityEngine;

public static class StaticRunOnGameLoad
{
    /// <summary>
    /// Gain performance on regular code execution outside of editor by disabling Debug logger.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void DisableLoggerOutsideOfEditor()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }
}