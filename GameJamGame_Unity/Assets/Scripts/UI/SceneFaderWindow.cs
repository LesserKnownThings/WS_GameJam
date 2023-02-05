using System.Collections;
using UnityEngine;

public class SceneFaderWindow : SubWindow
{
    [SerializeField]
    private CanvasGroup cg;
  
    public void SetAlpha(float newValue)
    {
        if(newValue > 0)
        {
            CallWindowAction();
        }
        else if(newValue <= 0)
        {
            BackAction();
        }

        cg.alpha = newValue;
    }
}