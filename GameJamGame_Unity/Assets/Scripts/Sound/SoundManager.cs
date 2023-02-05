using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get { return instance; } }
    private static SoundManager instance;

    [SerializeField]
    private SoundSubSystem defaultSoundSubsystem;
    public SoundSubSystem soundSubSystem { get; private set; }

    private AudioSource localSource;

    [SerializeField]
    private AudioClip hoverSound;
    [SerializeField]
    private AudioClip clickSound;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayUISound(bool isHover)
    {
        if(isHover)
        {
            localSource.PlayOneShot(hoverSound);
        }
        else
        {
            localSource.PlayOneShot(clickSound);
        }        
    }

    private void OnLevelWasLoaded(int level)
    {
        TryGetSoundSystem();
    }

    private void Start()
    {
        localSource = GetComponentInChildren<AudioSource>();
        TryGetSoundSystem();
    }

    private void TryGetSoundSystem()
    {
        soundSubSystem = FindObjectOfType<SoundSubSystem>();
        if (soundSubSystem == null)
        {
            soundSubSystem = Instantiate(defaultSoundSubsystem);
        }

        if (soundSubSystem != null)
        {
            soundSubSystem.PlayMusic(EWorldState.Futur);
        }
    }
}