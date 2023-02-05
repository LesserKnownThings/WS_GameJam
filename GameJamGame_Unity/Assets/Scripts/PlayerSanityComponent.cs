using System;
using Unity.Collections;
using UnityEngine;

public delegate void OnSanityChangeDelegate(int sanityValue);

public class PlayerSanityComponent : MonoBehaviour, IHUDInteractor
{
    public OnSanityChangeDelegate OnSanityChange;

    // Lets make total sanity a multiple of 4 at all times
    [SerializeField, Min(1), ReadOnly]
    private int maxSanity = 4;

    [SerializeField] 
    private int currentSanity;

    [SerializeField]
    private int sanityDecreaseRate = 1;

    [SerializeField]
    private HUDWindow sanityWindow;

    private PlayerMovementComponent _playerMovementComponent;    
    

    public MonoBehaviour GetOwner()
    {
        return this;
    }

    void Start()
    {
        currentSanity = maxSanity;
        _playerMovementComponent = GetComponent<PlayerMovementComponent>();

        World.Instance.GetHUD().AddSubWindow(sanityWindow, this);

        Invoke("Test", 1.5f);
    }

    private void Test()
    {
        ChangeSanity(2);
    }
    
    private void ChangeSanity(int sanityDecrease)
    {
        if (sanityDecrease == 0)
        {
            return;
        }

        currentSanity -= sanityDecrease;
        Math.Clamp(currentSanity, 0, maxSanity);
        Helper.InternalDebugLog("Sanity Changed");


        OnSanityChange?.Invoke(currentSanity);

        if (currentSanity <= 0)
        {
            // Game Over
        }
        if (sanityDecrease > 0)
        {
            
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Helper.InternalDebugLog("other hit");
        Transform otherTransform = other.transform;
        if (otherTransform.CompareTag("Enemy"))
        {
            Helper.InternalDebugLog("Player was hit");
            
            //What's up with the getter here?
            transform.GetComponent<PlayerSanityComponent>().ChangeSanity(sanityDecreaseRate);

            Vector2 knockBackDirection = (transform.position - otherTransform.position).normalized;
            
            _playerMovementComponent.AddKnockBackToMovement(knockBackDirection);
        }
    }    
}
