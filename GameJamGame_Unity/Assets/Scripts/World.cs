using UnityEngine;

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
    private UIManager uiManager;
    private UIManager uiManagerInternal;

    [SerializeField]
    private InputManager inputManager;
    private InputManager inputManagerInternal;
    
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
        GetUIManager();
    }
}