using System.Collections;
using UnityEngine;


public class SubWindowQuit : SubWindow
{
    private void Start()
    {
        windowType = WindowActionType.Quit;
    }

    public override void CallWindowAction()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}