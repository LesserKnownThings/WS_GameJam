using System.Collections;
using UnityEngine;


public class SubWindowStartGame : SubWindow
{
    public override void CallWindowAction()
    {
        UIManager.instance.StartGame();
    }
}