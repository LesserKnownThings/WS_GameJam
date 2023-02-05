using System.Timers;
using InteractionSystem;
using UnityEngine;

public class WorldSwitcherObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite rootSprite;
    [SerializeField] private Sprite wireSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        World.SwitchWorldDelegate += SwitchSprite;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTimer > 0)
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    [SerializeField]
    private float timeBeforeInteract = 3.0f;

    private float _currentTimer;
    
    public void Interact()
    {
        if( _currentTimer > 0)
        {
            return;
        }
        
        World.Instance.SwitchWorld();
        _currentTimer = timeBeforeInteract;
    }

    public void StopInteract()
    {
        
    }

    private SpriteRenderer _spriteRenderer;
    private void SwitchSprite(EWorldState worldState)
    {
        _spriteRenderer.sprite = worldState == EWorldState.Futur ? wireSprite : rootSprite;
        SoundManager.Instance.soundSubSystem.PlayMusic(worldState);
    }
}
