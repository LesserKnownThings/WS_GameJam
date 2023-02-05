using System.Collections.Generic;
using UnityEngine;

public enum WindowActionType
{
    MainMenu,
    SceneTransition,
    Settings,
    Credits,
    Quit,
    Back,
    Message
}

public class UIManager : MonoBehaviour
{
    protected SubWindow currentWindow;
    protected List<SubWindow> previousWindow = new List<SubWindow>();   

    protected Dictionary<WindowActionType, SubWindow> windows = new Dictionary<WindowActionType, SubWindow>();

    public bool TryGetWindow(WindowActionType type, out SubWindow window)
    {
        if(windows.TryGetValue(type, out window))
        {
            return true;
        }
        return false;
    }

    private void Awake()
    {
        SubWindow[] internalWindows = GetComponentsInChildren<SubWindow>();

        foreach (var window in internalWindows)
        {
            if(windows.ContainsKey(window.GetWindowType()))
            {
                Destroy(window.gameObject);
            }
            else
            {
                windows.Add(window.GetWindowType(), window);

                if(!window.IsMainWindow())
                {
                    window.gameObject.SetActive(false);
                }
                else
                {
                    currentWindow = window;
                }
            }
        }
    }

    //This should only be called by the button
    public virtual void CallAction(WindowActionType type)
    {
        if(windows.TryGetValue(type, out SubWindow window))
        {
            window.CallWindowAction();

            //If the window is an orphan one it shouldn't be managed by this flow
            if(currentWindow != null && !window.IsOrphanWidget())
            {
                previousWindow.Add(currentWindow);
                currentWindow.BackAction();
                currentWindow = window;
            }
        }
    }

    public virtual void BackAction()
    {
        currentWindow.BackAction();
        
        if(previousWindow.Count > 0)
        {
            currentWindow = previousWindow[previousWindow.Count - 1];
            previousWindow.RemoveAt(previousWindow.Count - 1);
            currentWindow.CallWindowAction();
        }
    }
}