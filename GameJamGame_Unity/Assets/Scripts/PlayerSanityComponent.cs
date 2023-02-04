using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;

public class PlayerSanityComponent : MonoBehaviour
{
    
    // Lets make total sanity a multiple of 4 at all times
    [SerializeField, Min(1), ReadOnly]
    private int maxSanity = 4;

    [SerializeField] private int currentSanity;

    private PlayerMovementComponent _playerMovementComponent;
    
    delegate void SanityChange(float sanityPercentage);
    SanityChange _myDelegate;
    
    // Start is called before the first frame update
    void Start()
    {
        currentSanity = maxSanity;
        _playerMovementComponent = GetComponent<PlayerMovementComponent>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeSanity(int sanityDecrease)
    {
        if (sanityDecrease == 0)
        {
            return;
        }

        currentSanity -= sanityDecrease;
        Math.Clamp(currentSanity, 0, maxSanity);
        Debug.Log("Sanity Changed");
        
        
        _myDelegate.Invoke(sanityPercentage: currentSanity/maxSanity);
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
        Debug.Log("other hit");
        Transform otherTransform = other.transform;
        if (otherTransform.CompareTag("Enemy"))
        {
            Debug.Log("Player was hit");
            
            transform.GetComponent<PlayerSanityComponent>().ChangeSanity(1);

            Vector2 knockBackDirection = (transform.position - otherTransform.position).normalized;
            
            _playerMovementComponent.AddKnockBackToMovement(knockBackDirection);
        }
    }
}
