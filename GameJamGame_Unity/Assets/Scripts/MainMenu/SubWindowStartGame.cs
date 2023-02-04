using System;
using System.Collections;
using UnityEngine;


public class SubWindowStartGame : SubWindow
{
    [Header("Only one of these 2 variables needs to be set in order to trigger change scene")]
    [SerializeField]
    private int levelToLoadIndex = -1;
    [SerializeField]
    private string levelToLoadName = "";

    public override void CallWindowAction()
    {
        base.CallWindowAction();

        UIManager.instance.StartGame(levelToLoadIndex, levelToLoadName);
    }
}