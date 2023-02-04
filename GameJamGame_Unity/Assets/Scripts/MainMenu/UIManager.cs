using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WindowActionType
{
    MainMenu,
    SceneTransition,
    Settings,
    Credits,
    Quit,
    Back
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    private SubWindow currentWindow;
    private List<SubWindow> previousWindow = new List<SubWindow>();

    Dictionary<WindowActionType, SubWindow> windows = new Dictionary<WindowActionType, SubWindow>();

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
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
    public void CallAction(WindowActionType type)
    {
        if(windows.TryGetValue(type, out SubWindow window))
        {
            window.CallWindowAction();

            if(currentWindow != null)
            {
                previousWindow.Add(currentWindow);
                currentWindow.BackAction();
                currentWindow = window;
            }
        }
    }

    public void BackAction()
    {
        currentWindow.BackAction();
        
        if(previousWindow.Count > 0)
        {
            currentWindow = previousWindow[previousWindow.Count - 1];
            currentWindow.CallWindowAction();
        }
    }

    public void StartGame(int sceneIndex = -1, string sceneName = "")
    {
        Helper.InternalDebugLog("Game Started", DebugType.Warning);

        StartCoroutine(LoadSceneRoutine(sceneIndex, sceneName));
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex = -1, string sceneName = "")
    {
        AsyncOperation operation = null;
        if (sceneIndex > -1)
        {
            operation = SceneManager.LoadSceneAsync(sceneIndex);
        }
        else if (!string.IsNullOrEmpty(sceneName))
        {
            operation = SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
            BackAction();
            yield return null;
        }

        if (operation != null)
        {
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                {
                    //This is just here to screw with players for 1.5 seconds because why not
                    yield return new WaitForSeconds(1.5f);
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}