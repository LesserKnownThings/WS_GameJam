using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public delegate void OnLoadLevelFailedDelegate();

public class World : MonoBehaviour
{
    #region Singleton
    public static World Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = new GameObject("World");
                return go.AddComponent<World>();
            }
            else
            {
                return instance;
            }
        }
    }
    private static World instance;
    #endregion

    [SerializeField] 
    private bool shouldShowUIOnStart = true;
    [SerializeField]
    private bool levelHasHUD = false;

    [SerializeField]
    private UIManager uiManager;
    private UIManager uiManagerInternal;

    [SerializeField]
    private InputManager inputManager;
    private InputManager inputManagerInternal;

    [SerializeField]
    private HUD hud;
    private HUD hudInternal;

    [Space(10)]
    [Header("Fading")]
    [SerializeField]
    private float fadeSpeed = 2.5f;
    [SerializeField]
    private float blackScreenDuration = 1.0f;

    public OnLoadLevelFailedDelegate OnLoadLevelFailed;

    public UIManager GetUIManager()
    {
        if(uiManagerInternal == null)
        {
            if (uiManager != null)
            {
                uiManagerInternal = Instantiate(uiManager);
            }
            //This is gonna create an empty UIManager, you don't want this most likely
            else
            {
                GameObject go = new GameObject("UI Manager");
                uiManagerInternal = go.AddComponent<UIManager>();
            }
        }

        return uiManagerInternal;
    }

    public InputManager GetInputManager()
    {
        if(inputManagerInternal == null)
        {
            if(inputManager != null)
            {
                inputManagerInternal = Instantiate(inputManager);
            }
            else
            {
                Helper.InternalDebugLog("The input manager is empty, please add an input manager", DebugType.Error);
            }
        }

        return inputManagerInternal;
    }

    public HUD GetHUD()
    {
        if(hudInternal == null && levelHasHUD)
        {
            if(hud != null)
            {
                hudInternal = Instantiate(hud);
            }
        }

        return hudInternal;
    }

    private void Awake()
    {
        GetUIManager();
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
        //Intializing the UI
        if (shouldShowUIOnStart)
        {
            GetUIManager();
        }
        else
        {
            GetUIManager().BackAction();
        }
        SwitchWorldDelegate?.Invoke(CurrentWorldState);

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
            OnLoadLevelFailed?.Invoke();
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
        
    public void FadeLevel(bool isFadeIn, Transform playerThatIsFading = null, Vector3 position = new Vector3())
    {
        GetInputManager().EnableInput(false);
        StartCoroutine(FadeRoutine(isFadeIn, playerThatIsFading, position));
    }

    private IEnumerator FadeRoutine(bool isFadeIn, Transform playerThatIsFading, Vector3 position)
    {
        float fadeInValue = isFadeIn ? 0f : 1f;

        if (GetUIManager().TryGetWindow(WindowActionType.Fade, out SubWindow subWindow))
        {
            SceneFaderWindow faderWindow = (SceneFaderWindow)subWindow;

            if (faderWindow != null)
            {
                if (isFadeIn)
                {
                    while (fadeInValue < 1f)
                    {
                        fadeInValue += Time.deltaTime * fadeSpeed;
                        faderWindow.SetAlpha(fadeInValue);
                        yield return null;
                    }

                    if(playerThatIsFading != null)
                    {
                        playerThatIsFading.position = position;
                    }

                    yield return new WaitForSeconds(blackScreenDuration);
                    FadeLevel(false);
                }
                else
                {
                    while (fadeInValue > 0f)
                    {
                        fadeInValue -= Time.deltaTime * fadeSpeed;
                        faderWindow.SetAlpha(fadeInValue);
                        yield return null;
                    }

                    GetInputManager().EnableInput(true);
                }
            }
        }
    }
    
    
    public static event Action<EWorldState> SwitchWorldDelegate;

    private EWorldState _currentWorldState = EWorldState.Past;

    public EWorldState CurrentWorldState
    {
        get => _currentWorldState;
    }
    
    [SerializeField] private GameObject[] pastTilemap;
    [SerializeField] private GameObject[] futureTilemap;

    public void SwitchWorld()
    {
        _currentWorldState = (_currentWorldState == EWorldState.Futur) ? EWorldState.Past : EWorldState.Futur;
        SwitchWorldDelegate?.Invoke(CurrentWorldState);

        foreach (var tilemap in pastTilemap)
        {
            tilemap.SetActive(_currentWorldState == EWorldState.Past);
        }
        foreach (var tilemap in futureTilemap)
        {
            tilemap.SetActive(_currentWorldState == EWorldState.Futur);
        }
        
        Debug.Log("Switch world: " + _currentWorldState);
    }
    
}