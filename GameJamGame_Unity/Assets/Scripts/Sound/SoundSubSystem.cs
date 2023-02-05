using System.Collections;
using UnityEngine;

public class SoundSubSystem : MonoBehaviour
{
    [SerializeField]
    private AudioClip mainSound;
    [SerializeField]
    private AudioClip secondarySound;
    [SerializeField]
    private bool onlyPlayMainSound = false;

    private AudioSource source;
    private float maxVolume;
    private const float fadeOutSpeed = 0.15f;

    private Coroutine fadeRoutine;
    private Coroutine firstFadeRoutine;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        maxVolume = source.volume;
        source.volume = 0.0f;
    }

    private void Start()
    {
        firstFadeRoutine = StartCoroutine(FirstFadeInRoutine());
    }

    public void PlayMusic(EWorldState state)
    {
        if(onlyPlayMainSound)
        {
            state = EWorldState.Futur;
        }

        if(fadeRoutine != null)
        {
            source.volume = 0.0f;
            StopCoroutine(fadeRoutine);
        }

        if(firstFadeRoutine != null)
        {
            source.volume = 0.0f;
            StopCoroutine(firstFadeRoutine);
            firstFadeRoutine = null;
        }

        fadeRoutine = StartCoroutine(FadeSound(state));
    }

    private IEnumerator FirstFadeInRoutine()
    {
        source.clip = mainSound;
        source.Play();

        while(source.volume < maxVolume)
        {
            source.volume += Time.deltaTime * fadeOutSpeed;
            yield  return null;
        }

        firstFadeRoutine = null;
    }

    private IEnumerator FadeSound(EWorldState state)
    {
        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime * fadeOutSpeed;
            yield return null;
        }

        source.Stop();

        if(state == EWorldState.Futur)
        {
            source.clip = mainSound;
        }
        else
        {
            source.clip = secondarySound;
        }
        
        source.Play();

        while (source.volume < maxVolume)
        {
            source.volume += Time.deltaTime * fadeOutSpeed;
            yield return null;
        }

        fadeRoutine = null;
    }
}