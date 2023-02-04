using System.Collections;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class InGameUIManager : UIManager
{
    private bool isOpen = false;

    private void Start()
    {
        World.Instance.GetInputManager().OnInputTriggered += OnInputTriggered;
    }

    private void OnInputTriggered(InputActionType type, bool isPressed)
    {
        switch (type)
        {
            case InputActionType.None:
                break;
            case InputActionType.LMC:
                break;
            case InputActionType.RMC:
                break;
            case InputActionType.MMC:
                break;
            case InputActionType.Escape:
                if (isPressed && currentWindow.GetWindowType() == WindowActionType.MainMenu)
                {
                    isOpen = !isOpen;
                    InGameMenuAction(isOpen);
                }
                break;
            case InputActionType.Action:
                break;
            default:
                break;
        }
    }

    private void InGameMenuAction(bool shouldOpen)
    {
        if (shouldOpen)
        {
            CallInputAction();
        }
        else
        {
            BackAction();
        }
    }

    private void CallInputAction()
    {
        if (windows.TryGetValue(WindowActionType.MainMenu, out SubWindow window))
        {
            window.CallWindowAction();
        }
    }

    public override void BackAction()
    {
        base.BackAction();
        isOpen = false;
    }
}