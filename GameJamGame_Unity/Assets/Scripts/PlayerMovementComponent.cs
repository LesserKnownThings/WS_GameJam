using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;

    private float _verticalValue;
    private float _horizontalValue;

    private Vector2 movementVector;
    
    [SerializeField] private float _speed = 1.0f;

    private InputManager _inputManager;

    [SerializeField] private Animator _animator;
    
    private void Start()
    {
        _inputManager = World.Instance.GetInputManager();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementVector = _inputManager.movementVector;
    }

    private string animationValName = "AnimationVal";
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        _rigidbody2D.MovePosition( position + _speed * Time.fixedDeltaTime * movementVector );

        if (movementVector.x == 0 && movementVector.y == 0)
        {
           // _animator.SetInteger(animationValName , -1);
            _animator.speed = 0;
        }
        else if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y))
        {
            _animator.speed = 1;
            _animator.SetInteger(animationValName, movementVector.x > 0 ? 0 : 1);
        }
        else
        {
            _animator.speed = 1;
            _animator.SetInteger(animationValName, movementVector.y > 0 ? 2 : 3);
        }

    }

    public void AddKnockBackToMovement(Vector2 knockBackDirection)
    {
        
    }
}
